using System;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace s3_application
{
    public class CreateCollection
    {
        public static void Example()
        {
           var rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.APSouth1);

            var collectionId = "BhavCollection";
            Console.WriteLine("Creating collection: " + collectionId);

            var createCollectionRequest = new CreateCollectionRequest()
            {
                CollectionId = collectionId
            };

            var createCollectionResponse =
                rekognitionClient.CreateCollectionAsync(createCollectionRequest);
            Console.WriteLine("CollectionArn : " + createCollectionResponse.Result.CollectionArn);
            Console.WriteLine("Status code : " + createCollectionResponse.Result.StatusCode);
        }
    }
}
