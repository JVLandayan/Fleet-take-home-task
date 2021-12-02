using Fleet.dto;
using Microsoft.AspNetCore.Hosting;

namespace Fleet.service.Third_Party;

public interface IUploadService
{
    public string SubmissionFiles(FileUploads uploadedFile);
    public IEnumerable<string> GetUploadedFiles();

    public IEnumerable<VehicleDto.VehicleRequests.UpdateVehicle> GetRecentUploadData();
}
public class UploadService : IUploadService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ICsvParser _csvparser;

    public UploadService(IWebHostEnvironment iWebHostEnvironment, ICsvParser csvParser)
    {
        _webHostEnvironment = iWebHostEnvironment;
        _csvparser = csvParser;
    }
    public string SubmissionFiles(FileUploads uploadedFile)
    {
        try
        {
            if (uploadedFile.files.Length > 0)
            {
                var path = _webHostEnvironment.ContentRootPath + "\\files\\";
                var fileExtension = Path.GetExtension(uploadedFile.files.FileName);
                var fileName = DateTime.Now.ToString("MM-dd-yyyy-H-mm") + Guid.NewGuid() + fileExtension;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                using (var fileStream = File.Create(Path.Combine(path, fileName)))
                {
                    uploadedFile.files.CopyTo(fileStream);
                    fileStream.Flush();
                    return fileName;
                }
            }

            return "uploading error";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public IEnumerable<string> GetUploadedFiles()
    {
        DirectoryInfo dir = new DirectoryInfo(_webHostEnvironment.ContentRootPath + "\\files\\");

        var fileNameList = new List<string>();
        FileInfo[] fileList = dir.GetFiles("*.csv");

        foreach (var file in fileList)
        {
            fileNameList.Add(file.Name);
        }

        fileNameList.Reverse();

        return fileNameList;
    }

    public IEnumerable<VehicleDto.VehicleRequests.UpdateVehicle> GetRecentUploadData()
    {
        DirectoryInfo dir = new DirectoryInfo(_webHostEnvironment.ContentRootPath + "\\files\\");

        var recentFile = (from file in dir.GetFiles()
            orderby file.LastWriteTime descending
            select file).First().Name;

        var csvData = _csvparser.CsvToJson(recentFile);

        return csvData;
    }
}