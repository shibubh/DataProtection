// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNet.Security.DataProtection
{
    internal unsafe sealed class DataProtectionProviderImpl : IDataProtectionProvider
    {
        private readonly byte[] _protectedKdk;

        public DataProtectionProviderImpl(byte[] protectedKdk)
        {
            _protectedKdk = protectedKdk;
        }

        public IDataProtector CreateProtector(string purpose)
        {
            BCryptKeyHandle newAesKeyHandle;
            BCryptHashHandle newHmacHashHandle;
            byte[] newProtectedKdfSubkey;

            BCryptUtil.DeriveKeysSP800108(_protectedKdk, purpose, Algorithms.AESAlgorithmHandle, out newAesKeyHandle, Algorithms.HMACSHA256AlgorithmHandle, out newHmacHashHandle, out newProtectedKdfSubkey);
            return new DataProtectorImpl(newAesKeyHandle, newHmacHashHandle, newProtectedKdfSubkey);
        }

        public void Dispose()
        {
            // no-op: we hold no protected resources
        }
    }
}
