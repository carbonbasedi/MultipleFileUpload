using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_PurpleBuzz.Areas.Admin.ViewModels.FeaturedWorkWFU;
using Task_PurpleBuzz.DAL;
using Task_PurpleBuzz.Models;
using Task_PurpleBuzz.Utilities.File;

namespace Task_PurpleBuzz.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/featured-work-wfu/{action=list}/{id?}")]
    public class FeaturedWorksWFUController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public FeaturedWorksWFUController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        [HttpGet]
        public IActionResult List()
        {
            var model = new FeaturedWorkWFUListVM
            {
                FeaturedWorkWFU = _context.FeaturedWorkWFU.FirstOrDefault()
            };
            return View(model);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var featuredWorkWFU = _context.FeaturedWorkWFU.FirstOrDefault();
            if (featuredWorkWFU != null) return BadRequest();
             
            return View();
        }

        [HttpPost]
        public IActionResult Create(FeaturedWorkWFUCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            foreach (var photo in model.Photos)
            {
                if (!_fileService.IsImage(photo))
                {
                    ModelState.AddModelError("Photos", "Wrong file format");
                    return View();
                }

                if (_fileService.IsBiggerThanSize(photo, 200))
                {
                    ModelState.AddModelError("Photos", "File size is over 200kb");
                    return View();
                }
            }

            var featuredWorkWFU = new FeaturedWorkWFU
            {
                Description = model.Description,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            _context.FeaturedWorkWFU.Add(featuredWorkWFU);

            int order = 1;
            foreach (var photo in model.Photos)
            {
                var featuredWorkWFUPhoto = new FeaturedWorkPhotoWFU
                {
                    Name = _fileService.Upload(photo),
                    Order = order++,
                    CreatedAt = DateTime.Now,
                    FeaturedWorkWFU = featuredWorkWFU
                };
                _context.FeaturedWorkPhotoWFUs.Add(featuredWorkWFUPhoto);
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var featuredWorkWFU = _context.FeaturedWorkWFU.Include(fw => fw.FeaturedWorkPhotoWFUs).FirstOrDefault(fw => fw.Id == id);
            if (featuredWorkWFU == null) return NotFound();

            var model = new FeaturedWorkWFUDetailsVM
            {
                Description = featuredWorkWFU.Description,
                CreatedAt = featuredWorkWFU.CreatedAt,
                ModifiedAt = featuredWorkWFU.ModifiedAt,
                Photos = featuredWorkWFU.FeaturedWorkPhotoWFUs.ToList(),
            };

            return View(model);
        }

		#endregion

		#region Delete
        public IActionResult Delete(int id)
        {
            var featuredWorkWFU = _context.FeaturedWorkWFU.Find(id);
            if(featuredWorkWFU is null) return NotFound();

            _context.FeaturedWorkWFU.Remove(featuredWorkWFU);
            _context.SaveChanges();

            return RedirectToAction(nameof(List));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var featuredWorkWFU = _context.FeaturedWorkWFU.Include(fw => fw.FeaturedWorkPhotoWFUs).FirstOrDefault(fw => fw.Id == id);
            if (featuredWorkWFU is null) return NotFound();

            var model = new FeaturedWorkWFUUpdateVM
            {
                Description = featuredWorkWFU.Description,
                Photos = featuredWorkWFU.FeaturedWorkPhotoWFUs.ToList()
            };
            
            return View(model); 
        }

        [HttpPost]
        public IActionResult Update(int id, FeaturedWorkWFUUpdateVM model)
        {
            if (!ModelState.IsValid) return View();

            var featuredWorkWFU = _context.FeaturedWorkWFU.Include(fw => fw.FeaturedWorkPhotoWFUs).FirstOrDefault(fw => fw.Id == id);
            if (featuredWorkWFU is null) return NotFound();

            featuredWorkWFU.Description = model.Description;
            _context.Update(featuredWorkWFU);

            foreach (var photo in model.Photos)
            {
                if (!_fileService.IsImage(photo))
                {
                    ModelState.AddModelError("Photos", "Wrong file format");
                    return View();
                }

                if (_fileService.IsBiggerThanSize(photo, 200))
                {
                    ModelState.AddModelError("Photos", "File size is over 200kb");
                    return View();
                }
            }

            var lastOrder = featuredWorkWFU.FeaturedWorkPhotoWFUs.OrderByDescending(p => p.Order).FirstOrDefault()?.Order;
            int order = 1;
            foreach (var photo in model.Photos)
            {
                var featuredWorkPhotoWFU = new FeaturedWorkPhotoWFU
                {
                    Name = _fileService.Upload(photo),
                    CreatedAt = DateTime.Now,
                    FeaturedWorkWFU = featuredWorkWFU,
                    Order = lastOrder is not null ? (int)(++lastOrder) : order++
                };

                _context.FeaturedWorkPhotoWFUs.Add(featuredWorkPhotoWFU);
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), "featuredworkwfu", new { id = featuredWorkWFU.Id });
        }
		#endregion
	}
}
