using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace s3_application.Helpers
{
    public class StorageOperations
    {
        public static void CreateBucket(AmazonS3Client s3, string bucketName)
        {
            PutBucketRequest req = new PutBucketRequest
            {
                BucketName = bucketName,
                BucketRegion = S3Region.APS1
            };

            Task<PutBucketResponse> res = s3.PutBucketAsync(req);

            Task.WaitAll(res);

            if (res.IsCompletedSuccessfully)
            {
                Console.WriteLine("New S3 bucket created: {0}", bucketName);
            }
        }

        public static void WriteObject(AmazonS3Client s3, string bucketName, string key)
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes("Test S3 data"));

            PutObjectRequest req = new PutObjectRequest();
            req.BucketName = bucketName;
            req.Key = key;
            req.InputStream = ms;

            Task<PutObjectResponse> res = s3.PutObjectAsync(req);

            Task.WaitAll(res);

            if (res.IsCompletedSuccessfully)
            {
                Console.WriteLine("Created object '{0}' in bucket '{1}'", key, bucketName);
            }
        }

        public static void ReadObject(AmazonS3Client s3, string bucketName, string key)
        {
            GetObjectRequest req = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            Task<GetObjectResponse> res = s3.GetObjectAsync(req);

            Task.WaitAll(res);
            if (res.IsCompletedSuccessfully)
            {
                using (TextReader tr = new StreamReader(res.Result.ResponseStream))
                {
                    Console.WriteLine("Retrieved contents of object '{0}' in bucket '{1}'", key, bucketName);
                    Console.WriteLine(tr.ReadToEnd());
                }
            }
        }

        public static void ListBuckets(AmazonS3Client s3)
        {
            ListBucketsRequest req = new ListBucketsRequest();

            Task<ListBucketsResponse> res = s3.ListBucketsAsync(req);

            Task.WaitAll(res);

            Console.WriteLine("List of S3 Buckets in your AWS Account");
            foreach (var bucket in res.Result.Buckets)
            {
                Console.WriteLine(bucket.BucketName);
            }
        }

        public static void ListObjects(AmazonS3Client s3, string bucketName)
        {
            ListObjectsRequest req = new ListObjectsRequest
            {
                BucketName = bucketName,
                MaxKeys = 100
            };

            Task<ListObjectsResponse> res = s3.ListObjectsAsync(req);

            Task.WaitAll(res);

            Console.WriteLine("List of objects in your S3 Bucket '{0}'", bucketName);


            foreach (var s3Object in res.Result.S3Objects)
            {
                Console.WriteLine(s3Object.Key);
            }
        }

        public static void DeleteObject(AmazonS3Client s3, string bucketName, string key)
        {
            DeleteObjectRequest req = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            Task<DeleteObjectResponse> res = s3.DeleteObjectAsync(req);

            Task.WaitAll(res);

            if (res.IsCompletedSuccessfully)
            {
                Console.WriteLine("Deleted object '{0}' from bucket '{1}'", key, bucketName);
            }
        }

        public static void DeleteBucket(AmazonS3Client s3, string bucketName)
        {
            DeleteBucketRequest req = new DeleteBucketRequest
            {
                BucketName = bucketName
            };

            Task<DeleteBucketResponse> res = s3.DeleteBucketAsync(req);

            Task.WaitAll(res);

            if (res.IsCompletedSuccessfully)
            {
                Console.WriteLine("Deleted bucket - '{0}'", bucketName);
            }
        }
        public static void DeleteFolder(AmazonS3Client s3, string bucketName, string keysAndVersions)
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = keysAndVersions
            };
            Task<GetObjectResponse> res = s3.GetObjectAsync(request);

            Task.WaitAll(res);
            if (res.IsCompletedSuccessfully)
            {
                DeleteObjectsRequest multiObjectDeleteRequest = new DeleteObjectsRequest
                {
                    BucketName = bucketName,
                    // This includes the object keys and null version IDs.
                };

                // You can add specific object key to the delete request using the .AddKey.
                // multiObjectDeleteRequest.AddKey("TickerReference.csv", null);

                Task<DeleteObjectsResponse> response = s3.DeleteObjectsAsync(multiObjectDeleteRequest);
                //Console.WriteLine("Successfully deleted all the {0} items", response.De.Count);
            }
        }

       private static void PrintDeletionErrorStatus(DeleteObjectsException e)
        {
            // var errorResponse = e.ErrorResponse;
            DeleteObjectsResponse errorResponse = e.Response;
            Console.WriteLine("x {0}", errorResponse.DeletedObjects.Count);

            Console.WriteLine("No. of objects successfully deleted = {0}", errorResponse.DeletedObjects.Count);
            Console.WriteLine("No. of objects failed to delete = {0}", errorResponse.DeleteErrors.Count);

            Console.WriteLine("Printing error data...");
            foreach (DeleteError deleteError in errorResponse.DeleteErrors)
            {
                Console.WriteLine("Object Key: {0}\t{1}\t{2}", deleteError.Key, deleteError.Code, deleteError.Message);
            }
        }


    }
}
