﻿// ***********************************************************************
// Copyright (c) 2021 Charlie Poole, Rob Prouse
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************
using System;
using SIO = System.IO;

namespace NUnit.Engine.Internal.FileSystemAccess.Default
{
    /// <summary>
    /// Default implementation of <see cref="IFileSystem"/> that relies on <see cref="System.IO"/>.
    /// </summary>
    internal sealed class FileSystem : IFileSystem
    {
        /// <inheritdoc/>
        public bool Exists(IDirectory directory)
        {
            if (directory == null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            return SIO.Directory.Exists(directory.FullName);
        }

        /// <inheritdoc/>
        public bool Exists(IFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return SIO.File.Exists(file.FullName);
        }

        /// <inheritdoc/>
        public IDirectory GetDirectory(string path)
        {
            if (SIO.Directory.Exists(path))
            {
                return new Directory(path);
            }
            else
            {
                throw new SIO.DirectoryNotFoundException(string.Format("Directory '{0}' not found.", path));
            }
        }

        /// <inheritdoc/>
        public IFile GetFile(string path)
        {
            var directory = SIO.Path.GetDirectoryName(path);
            if (SIO.Directory.Exists(directory))
            {
                return new File(path);
            }
            else
            {
                throw new SIO.DirectoryNotFoundException(string.Format("Directory '{0}' not found.", directory));
            }
        }
    }
}
