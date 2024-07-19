using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.Models;

namespace Q2.Pages.Course
{
    public class AddWithUserModel : PageModel
    {
        private readonly PE_PRN_24SumB1Context _context;

        public AddWithUserModel(PE_PRN_24SumB1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Instructor> Instructors { get; set; } = new();

        [BindProperty]
        public List<User> Users { get; set; } = new();

        public IActionResult OnGet()
        {
            Instructors = _context.Instructors.ToList();
            Users = _context.Users.ToList();
            return Page();
        }


        [BindProperty]
        public CourseRequest AddCourseRequest {  get; set; }
        public class CourseRequest
        {
            public string? Title { get; set; }
            public string? Description { get; set; }
            public int? InstructorId { get; set; }
            public string? UserIds { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var course = new Models.Course()
            {
                Title = AddCourseRequest.Title,
                Description = AddCourseRequest.Description,
                InstructorId = AddCourseRequest.InstructorId,
            };

            _context.Add(course);
            await _context.SaveChangesAsync();

            if (!String.IsNullOrEmpty(AddCourseRequest.UserIds?.Trim()))
            {
                var userIds = AddCourseRequest.UserIds.Trim().Split(" ");
                foreach (var id in userIds)
                {
                    _context.Add(new Enrollment()
                    {
                        CourseId = course.CourseId,
                        UserId = Int32.Parse(id),
                        EnrolledDate = DateTime.Now,
                    });
                }

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(OnGet));

        }
    }
}
