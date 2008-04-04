// $Id: Program.cs 542 2008-01-04 00:22:02Z ho $
using System;
using System.Globalization; 
using System.Collections.Generic;
using System.Text;
using ILNumerics.Test;
using ILNumerics.Misc; 
using ILNumerics; 
using ILNumerics.BuiltInFunctions; 
using ILNumerics.Exceptions;
using ILNumerics.Native;  

namespace ILNumerics.Test {
	class Program : ILNumerics.Algorithms.ILAlgorithm {
        // (Un)comment needed or not needed tests!
        /// <summary>
        /// Mains the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        static void Main(string[] args) {
        	Console.Out.Write(Info());
            //new TESTILBinTreeNode().Run();
            IILLapack oldLapack = Lapack; 
            Lapack = new ILLapackGeneric(); 
            new TESTILLapack().Run();
            Lapack = new ILLapackMKL10_0(); 
            new TESTILLapack().Run();
            Lapack = new ILACML3_6(); 
            new TESTILLapack().Run();
            Lapack = oldLapack; 
            
            new TESTILMatFile().Run(); 
            new TESTILArray().Run();
            new TESTILCell().Run(); 
            new TESTBucketSort().Run(); 
            new TESTQuickSort().Run(); 
            new TESTILMath().Run();
            new TESTILMemoryPool().Run(); 
            new TESTILAlgorithm().Run();
            new TESTILNumericArray().Run();
            new TESTLogical().Run(); 
            new TESTILStorage().Run();
            //new TESTILWorkbench().Run();
            Console.Out.WriteLine("Done.");
			Console.In.ReadLine();
		}
        
        
        private static string Info() {
        	StringBuilder s = new StringBuilder(); 
        	s.Append("This is the general test routine for ILNumerics.Net" + Environment.NewLine); 
        	s.Append("" + Environment.OSVersion + ", "); 
        	s.Append("Number of proc: " + Environment.ProcessorCount + ", "); 
        	s.Append("CLR: " + Environment.Version + ", "); 
        	s.Append("Proc.memory: " + Environment.WorkingSet + "");
        	return s.ToString(); 
        }

        /// <summary>
        /// compute esnesnon34 - conv implace
        /// </summary>
        /// <param name="A">reliability convolution sum</param>
        /// <param name="B">process integration factors</param>
        /// <returns>MaxConv34 weights as interop solution</returns>
        /// <remarks>A &amp; B must have the same size</remarks>
        public static ILArray<double> MaxConv34 (ILArray<double> A, ILArray<double> B) {
            if (!B.Dimensions.IsSameSize(A.Dimensions) ) 
                throw new ILArgumentException("Input argument B must have the same size as A!");
            ILArray<double> ret = zeros(A.Dimensions); 
            ret = eigSymm(B);
            ret += repmat(B[null,0],4,A.Dimensions[1]);
            ret = diag(diag(ret) * exp(A["end;:"]));
            return ret;
        }
	}
}