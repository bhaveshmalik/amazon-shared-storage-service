using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using s3_application.Helpers;
using System;

namespace s3_application
{
    class Program
    {
        //static readonly string bucketName = "bucket-" + Guid.NewGuid().ToString("n").Substring(0, 8);
        //static readonly string key = "key-" + Guid.NewGuid().ToString("n").Substring(0, 8);

        static readonly string bucketName = "bhavesh-aws-bucket";
        static readonly string key = "root/profile/";
        static readonly string newKey = "root/profile/newFile.txt";
        

        static void Main(string[] args)
        {
            //var sharedFile = new SharedCredentialsFile();
            //if (sharedFile.TryGetProfile("default", out CredentialProfile basicProfile) &&
            //    AWSCredentialsFactory.TryGetAWSCredentials(basicProfile, sharedFile, out AWSCredentials awsCredentials))
            //{
            //}
            //var client = new AmazonS3Client(RegionEndpoint.APSouth1);

            AddFaces.Example();
            //StorageOperations.CreateBucket(client, bucketName);
            //Console.WriteLine("Press enter to continue...");
            //Console.Read();

            //StorageOperations.ListBuckets(client);
            //Console.WriteLine("Press enter to continue...");
            //Console.Read();

            //StorageOperations.WriteObject(client, bucketName, newKey);
            //Console.WriteLine("Press enter to continue...");
            //Console.Read();

            //StorageOperations.ListObjects(client, bucketName);
            //Console.WriteLine("Press enter to continue...");
            //Console.Read();

            //StorageOperations.ReadObject(client, bucketName, key);
            //Console.WriteLine("Press enter to continue...");
            //Console.Read();

            //StorageOperations.DeleteFolder(client, bucketName, key);
            //Console.WriteLine("Press enter to continue...");
            //Console.Read();

            //StorageOperations.DeleteBucket(client, bucketName);
        }
    }
}
