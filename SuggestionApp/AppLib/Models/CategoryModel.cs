namespace AppLib.Models
{
    public class CategoryModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string CategotyName { get; set; } 
        public string CategoryDescription { get; set; }

    }
}
