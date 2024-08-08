using Microsoft.AspNetCore.Mvc;

namespace VormerIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private static IWebHostEnvironment _webHostEnvironment;

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadFile obj)
        {
            // Use a directory under the wwwroot folder to ensure it's writable
            string directory = Path.Combine(_webHostEnvironment.WebRootPath, "Files");

            if (obj.files != null && obj.files.Length > 0)
            {
                try
                {
                    // Ensure the directory exists
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Combine the directory with the file name to create the full path
                    string filePath = Path.Combine(directory, obj.files.FileName);

                    // Create and save the file
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await obj.files.CopyToAsync(fileStream);
                    }

                    // Return the relative path to the file
                    string relativePath = Path.Combine("Files", obj.files.FileName);
                    return Ok(new { FilePath = relativePath });
                }
                catch (Exception e)
                {
                    return StatusCode(500, $"Internal server error: {e.Message}");
                }
            }
            else
            {
                return BadRequest("Upload failed: No file was selected.");
            }
        }
    }
}