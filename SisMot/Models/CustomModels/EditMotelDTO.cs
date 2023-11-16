namespace SisMot.Models.CustomModels;

public class EditMotelDTO
{
    public int MotelId { get; set; }
    public string MotelName { get; set; }
    public string MotelDescription { get; set; }
    public string MotelPrice { get; set; }
    public string MotelPhoneNumber { get; set; }
    public double MotelLatitude { get; set; }
    public double MotelLongitude { get; set; }
    public List<IFormFile> MotelPhotos { get; set; }
    public List<string> PhotosPaths { get; set; }
}