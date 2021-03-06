// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;

namespace Microsoft.AspNet.Security.DataProtection
{
    internal sealed class DpapiDataProtectionProviderImpl : IDataProtectionProvider
    {
        private readonly byte[] _entropy;
        private readonly bool _protectToLocalMachine;

        public DpapiDataProtectionProviderImpl(byte[] entropy, bool protectToLocalMachine)
        {
            Debug.Assert(entropy != null);
            _entropy = entropy;
            _protectToLocalMachine = protectToLocalMachine;
        }

        public IDataProtector CreateProtector(string purpose)
        {
            return new DpapiDataProtectorImpl(BCryptUtil.GenerateDpapiSubkey(_entropy, purpose), _protectToLocalMachine);
        }

        public void Dispose()
        {
            // no-op; no unmanaged resources to dispose
        }
    }
}
