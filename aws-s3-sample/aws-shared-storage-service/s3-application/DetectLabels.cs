using System;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace s3_application
{
    public class DetectLabels
    {
        public static void Example()
        {
            const string photo = "recognition/BM.jpg";
            const string bucket = "bhavesh-aws-bucket";

            var rekognitionClient = new AmazonRekognitionClient(RegionEndpoint.APSouth1);

            var detectlabelsRequest = new DetectLabelsRequest()
            {
                Image = new Image()
                {
                    S3Object = new S3Object()
                    {
                        Name = photo,
                        Bucket = bucket
                    },
                },
                MaxLabels = 10,
                MinConfidence = 75F
            };

            try
            {
                var detectLabels = rekognitionClient.DetectLabelsAsync(detectlabelsRequest);
                var detectLabelsResponse = detectLabels.Result;
                Console.WriteLine("Detected labels for " + photo);
                foreach (Label label in detectLabelsResponse.Labels)
                    Console.WriteLine("{0}: {1}", label.Name, label.Confidence);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
}
}
