using System.Diagnostics;
using System.Text.Json;
using Business.Entities;
using Business.Interfaces;

namespace Business.Repositories;

public class ContactRepository(IContactFileService contactFileService) : BaseRepository<ContactEntity>, IContactRepository
{
    private readonly IContactFileService _contactFileService = contactFileService;

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
        var list = Deserialize(json);
        return list;
    }
}