﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalRuby.IPBanCore
{
    /// <summary>
    /// Range of ipv6 addresses
    /// </summary>
    public struct IPV6Range : IComparable<IPV6Range>
    {
        /// <summary>
        /// Begin ip address
        /// </summary>
        public UInt128 Begin;

        /// <summary>
        /// End ip address
        /// </summary>
        public UInt128 End;

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (int)(Begin.GetHashCode() + End.GetHashCode());
            }
        }

        /// <summary>
        /// Check for equality
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if equal, false otherwise</returns>

        public override bool Equals(object obj)
        {
            if (!(obj is IPV6Range range))
            {
                return false;
            }
            return Begin == range.Begin && End == range.End;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="range">IPAddressRange</param>
        /// <exception cref="InvalidOperationException">Invalid address family</exception>
        public IPV6Range(IPAddressRange range)
        {
            if (range.Begin.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                throw new InvalidOperationException("Wrong address family for an ipv4 range");
            }
            Begin = range.Begin.ToUInt128();
            End = range.End.ToUInt128();
        }

        /// <summary>
        /// Conver to an ip address range
        /// </summary>
        /// <returns>IPAddressRange</returns>
        public IPAddressRange ToIPAddressRange() => new IPAddressRange(Begin.ToIPAddress(), End.ToIPAddress());

        /// <summary>
        /// IComparer against another IPV4Range
        /// </summary>
        /// <param name="other">Other range</param>
        /// <returns></returns>
        public int CompareTo(IPV6Range other)
        {
            int cmp = End.CompareTo(other.Begin);
            if (cmp < 0)
            {
                return cmp;
            }
            cmp = Begin.CompareTo(other.End);
            if (cmp > 0)
            {
                return cmp;
            }

            // inside range
            return 0;
        }
    }
}