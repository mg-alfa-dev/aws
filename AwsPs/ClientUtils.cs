﻿using System.Collections.Generic;
using System.Linq;
using System.Security;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace AwsPs
{
    public static class ClientProvider
    {
        public static IClient GetClient()
        {
            return new Client();
        }
    }

    public interface IClient
    {
        IEnumerable<Instance> GetInstances();
        void StartInstance(string instanceId);
        void StopInstance(string instanceId);
    }

    public class FakeClient : IClient
    {
        public IEnumerable<Instance> GetInstances()
        {
            return new[]
                       {
                            new Instance{ Name = "Service 1", Id = "ip-123456", Dns = "my-dns-is-really-long-hello-hello", Ip = "192.123.531.23", State = "running" },
                            new Instance{ Name = "Service 2", Id = "ip-123456", Dns = "my-dns-is-really-long-hello-hello", Ip = "192.123.531.23", State = "running" },
                            new Instance{ Name = "Service 3 has long name", Id = "ip-123456", Dns = "my-dns-is-really-long-hello-hello", Ip = "192.123.531.23", State = "running" },
                            new Instance{ Name = "Service 4", Id = "ip-123456", Dns = "my-dns-is-really-long-hello-hello", Ip = "192.123.531.23", State = "running" },
                            new Instance{ Name = "Service 5", Id = "ip-123456", Dns = "my-dns-is-really-long-hello-hello", Ip = "192.123.531.23", State = "running" },
                       };
        }

        public void StartInstance(string instanceId)
        {
        }

        public void StopInstance(string instanceId)
        {
        }
    }

    public class Client : IClient
    {
        public IEnumerable<Instance> GetInstances()
        {
            AmazonEC2Client client = _GetClient();
            var describeInstancesRequest = new DescribeInstancesRequest();
            var response = client.DescribeInstances(describeInstancesRequest);
            var instances = response.DescribeInstancesResult.Reservation
                .SelectMany(x => x.RunningInstance)
                .Select(x =>
                        new Instance
                        {
                            Name = x.Tag.Where(y => y.Key == "Name").Select(z => z.Value).First(),
                            Id = x.InstanceId,
                            State = x.InstanceState.Name,
                            Ip = x.IpAddress,
                            Dns = x.PublicDnsName,
                        });
            return instances;
        }

        public void StartInstance(string instanceId)
        {
            var client = _GetClient();
            client.StartInstances(new StartInstancesRequest {InstanceId = new List<string> {instanceId}});
        }

        public void StopInstance(string instanceId)
        {
            var client = _GetClient();
            client.StopInstances(new StopInstancesRequest {InstanceId = new List<string> {instanceId}});
        }

        private static AmazonEC2Client _GetClient()
        {
            var awsAccessKeyId = "ACCESS_KEY";
            var secretAccessKey = "SECRET_ACCESS_KEY".ToCharArray();
            var awsSecretAccessKey = new SecureString();
            foreach (var secretAccessKeyChar in secretAccessKey)
                awsSecretAccessKey.AppendChar(secretAccessKeyChar);
            awsSecretAccessKey.MakeReadOnly();
            var config = new AmazonEC2Config();
            return new AmazonEC2Client(awsAccessKeyId, awsSecretAccessKey, config);
        }
    }
}
