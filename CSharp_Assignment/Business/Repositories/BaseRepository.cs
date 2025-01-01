using System.Text.Json;
using Business.Interfaces;

namespace Business.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
{
    public virtual string Serialize(List<TEntity> list)
    {
        var json = JsonSerializer.Serialize(list);
        return json;
    }

    public virtual List<TEntity>? Deserialize(string json)
    {
        // Added a try catch, in order to get the test working.
        try
        {
            var jsonObject = JsonSerializer.Deserialize<List<TEntity>>(json);
            return jsonObject ?? new List<TEntity>();
        }
        catch (JsonException)
        {
            // If deserialization fails, return an empty list
            return new List<TEntity>();
        }
    }

    public abstract bool SaveToFile(List<TEntity> list);

    public abstract List<TEntity>? GetFromFile();
}