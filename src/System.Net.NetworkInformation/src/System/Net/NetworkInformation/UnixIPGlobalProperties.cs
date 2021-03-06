﻿using System.Threading;
using System.Threading.Tasks;

namespace System.Net.NetworkInformation
{
    internal abstract class UnixIPGlobalProperties : IPGlobalProperties
    {
        public sealed override Task<UnicastIPAddressInformationCollection> GetUnicastAddressesAsync()
        {
            return Task.Factory.StartNew(s => ((UnixIPGlobalProperties)s).GetUnicastAddresses(), this,
                CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }

        private unsafe UnicastIPAddressInformationCollection GetUnicastAddresses()
        {
            UnicastIPAddressInformationCollection collection = new UnicastIPAddressInformationCollection();

            Interop.Sys.EnumerateInterfaceAddresses(
                (name, ipAddressInfo, netmaskInfo) =>
                {
                    IPAddress ipAddress = IPAddressUtil.GetIPAddressFromNativeInfo(ipAddressInfo);
                    if (!IPAddressUtil.IsMulticast(ipAddress))
                    {
                        IPAddress netMaskAddress = IPAddressUtil.GetIPAddressFromNativeInfo(netmaskInfo);
                        collection.InternalAdd(new UnixUnicastIPAddressInformation(ipAddress, netMaskAddress));
                    }
                },
                (name, ipAddressInfo, scopeId) =>
                {
                    IPAddress ipAddress = IPAddressUtil.GetIPAddressFromNativeInfo(ipAddressInfo);
                    if (!IPAddressUtil.IsMulticast(ipAddress))
                    {
                        collection.InternalAdd(new UnixUnicastIPAddressInformation(ipAddress, IPAddress.Any));
                    }
                },
                // Ignore link-layer addresses that are discovered; don't create a callback.
                null);

            return collection;
        }
    }
}
