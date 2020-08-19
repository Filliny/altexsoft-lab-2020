namespace Recipes.Models
{
    //Serializing class must have file name property 
    public interface IDataserializable 
    {
        string DbFilename { get; }
    } 

}
