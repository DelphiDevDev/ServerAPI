using System;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;


namespace ClassLibraryAPI
{
    public class JSONUtils
    {
        public static string BadRequestJSON(int Type)
        {
            dynamic AnswerJSON = new System.Dynamic.ExpandoObject();
            AnswerJSON.success = false;
            switch (Type) {
                case 1:
                    AnswerJSON.error = "Bad GET request";
                    break;
                case 2:
                    AnswerJSON.error = "Missing parameters for GET list request"; 
                    break;
                case 3:
                    AnswerJSON.error = "Unsupported method: ";
                    break;
                default:
                    AnswerJSON.error = "Unknown error";
                    break;
            }

            return JsonSerializer.Serialize(AnswerJSON);
        }

        public static string GETResponseJSON(string fromid, string toid)
        {
            int fromidInt = int.Parse(fromid);
            int toidInt = int.Parse(toid);

            string filePath = "data.json";
            string jsondata = File.ReadAllText(filePath);

            JsonNode jsonObject = JsonNode.Parse(jsondata)!;
            JsonNode root = jsonObject.Root;
            JsonArray products = root["products"]!.AsArray();

            dynamic AnswerJSON = new System.Dynamic.ExpandoObject();

            List<dynamic> SelectedProducts = new List<dynamic>();

            int id = 0;
            string productName = "";
            int quantity = 0;
            double price = 0.0f;

            foreach (JsonNode? product in products)
            {
                if (product?["id"] is JsonNode idNode)
                {
                    id = (int)idNode;
                }
                if (product?["name"] is JsonNode nameNode)
                {
                    productName = (string)nameNode;
                }
                if (product?["quantity"] is JsonNode quantityNode)
                {
                    quantity = (int)quantityNode;
                }
                if (product?["price"] is JsonNode priceNode)
                {
                    price = (double)priceNode;
                }

                if (id >= fromidInt && id <= toidInt) 
                {
                    dynamic SelectedProduct = new System.Dynamic.ExpandoObject();
                    SelectedProduct.id = id;
                    SelectedProduct.name = productName;
                    SelectedProduct.quantity = quantity;
                    SelectedProduct.price = price;
                    SelectedProducts.Add(SelectedProduct);
                }

                AnswerJSON.success = true;
                AnswerJSON.products = SelectedProducts;
            }
            
            return JsonSerializer.Serialize(AnswerJSON);
        }
    }
}
