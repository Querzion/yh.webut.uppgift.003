using System.Text.Json;
using Business.Interfaces;

namespace Business.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
{
    public string Serialize(List<TEntity> list)
    {
        var json = JsonSerializer.Serialize(list);
        return json;
    }

    public List<TEntity>? Deserialize(string json)
    {
        var jsonObject = JsonSerializer.Deserialize<List<TEntity>>(json);
        return jsonObject;
    }

    public abstract bool SaveToFile(List<TEntity> list);

    public abstract List<TEntity>? GetFromFile();
}