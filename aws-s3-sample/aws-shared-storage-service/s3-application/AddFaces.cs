using System;
using System.Collections.Generic;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace s3_application
{
    public class AddFaces
    {
        public static void Example()
        {
            String collectionId = "BhavCollection";
            String bucket = "bhavesh-aws-bucket";
            String photo = "Malik.jpg";

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.APSouth1);

            Image image = new Image()
            {
                S3Object = new S3Object()
                {
                    Bucket = bucket,
                    Name = photo
                }
            };

            IndexFacesRequest indexFacesRequest = new IndexFacesRequest()
            {
                Image = image,
                CollectionId = collectionId,
                ExternalImageId = photo,
                DetectionAttributes = new List<String>() {"ALL"}
            };

            var indexFacesResponse = rekognitionClient.IndexFacesAsync(indexFacesRequest);

            Console.WriteLine(photo + " added");
            foreach (FaceRecord faceRecord in indexFacesResponse.Result.FaceRecords)
                Console.WriteLine("Face detected: Faceid is " +
                                  faceRecord.Face.FaceId);
        }
    }

}