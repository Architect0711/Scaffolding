﻿using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using Microsoft.Dnx.Runtime;
using Microsoft.Extensions.CodeGeneration;
using Microsoft.Extensions.CodeGeneration.EntityFramework;

namespace Microsoft.Extensions.CodeGenerators.Mvc
{
    /// <summary>
    /// Common generator functionality for Controllers and Views
    /// </summary>
    public abstract class CommonGeneratorBase
    {
        protected CommonGeneratorBase([NotNull]IApplicationEnvironment applicationEnvironment)
        {
            ApplicationEnvironment = applicationEnvironment;
        }

        protected IApplicationEnvironment ApplicationEnvironment
        {
            get;
            private set;
        }

        protected string ValidateAndGetOutputPath(CommonCommandLineModel commandLineModel, string outputFileName)
        {
            string outputFolder = String.IsNullOrEmpty(commandLineModel.RelativeFolderPath)
                ? ApplicationEnvironment.ApplicationBasePath
                : Path.Combine(ApplicationEnvironment.ApplicationBasePath, commandLineModel.RelativeFolderPath);

            var outputPath = Path.Combine(outputFolder, outputFileName);

            if (File.Exists(outputPath) && !commandLineModel.Force)
            {
                throw new InvalidOperationException(string.Format(
                    CultureInfo.CurrentCulture,
                    "The file {0} exists, use -f option to overwrite",
                    outputPath));
            }

            return outputPath;
        }
    }
}
