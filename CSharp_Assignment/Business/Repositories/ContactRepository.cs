using System.Diagnostics;
using System.Text.Json;
using Business.Entities;
using Business.Interfaces;

namespace Business.Repositories;

public class ContactRepository : BaseRepository<ContactEntity>, IContactRepository
{
    private readonly IContactFileService _contactFileService;

    public ContactRepository(IContactFileService contactFileService)
    {
        _contactFileService = contactFileService;
    }

    public override bool SaveToFile(List<ContactEntity> list)
    {
        try
        {
            var json = JsonSerializer.Serialize(list);
            _contactFileService.SaveContentToFile(json);
            
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public override List<ContactEntity>? GetFromFile()
    {
        var json = _contactFileService.GetContentFromFile();
        
        // Check if the JSON content is null or empty (Based on one of the ChatGPT tests for the ContactRepository,
        // it seems I have to send an empto list if the content is null. And now the test is successful)
        if (string.IsNullOrEmpty(json))
        {
            return new List<ContactEntity>();  // Return an empty list if no content is found
        }
        
        var list = Deserialize(json);
        return list;
    }
}