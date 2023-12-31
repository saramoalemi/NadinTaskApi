﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NadinTask.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Shared {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Shared() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NadinTask.Domain.Resources.Shared", typeof(Shared).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to فعال.
        /// </summary>
        public static string Active {
            get {
                return ResourceManager.GetString("Active", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to کد.
        /// </summary>
        public static string Code {
            get {
                return ResourceManager.GetString("Code", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مقدار گذرواژه ها با هم برابر نیست.
        /// </summary>
        public static string ComparePasswordError {
            get {
                return ResourceManager.GetString("ComparePasswordError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to نوع فایل انتخابی برای {0} صحیح نیست.
        /// </summary>
        public static string FileTypeError {
            get {
                return ResourceManager.GetString("FileTypeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مقدار {0} را با فرمت صحیح وارد کنید.
        /// </summary>
        public static string FormatError {
            get {
                return ResourceManager.GetString("FormatError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حداکثر طول مجاز {0} برابر با {1} کاراکتر است.
        /// </summary>
        public static string MaxLengthError {
            get {
                return ResourceManager.GetString("MaxLengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حداقل {1} کاراکتر لازم است.
        /// </summary>
        public static string MinLengthError {
            get {
                return ResourceManager.GetString("MinLengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to کد ملی.
        /// </summary>
        public static string National_code {
            get {
                return ResourceManager.GetString("National_code", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مقدار {0} باید بین {1} و {2} باشد.
        /// </summary>
        public static string RangeError {
            get {
                return ResourceManager.GetString("RangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مقدار &apos;{0}&apos; الزامی است.
        /// </summary>
        public static string RequiredError {
            get {
                return ResourceManager.GetString("RequiredError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مقدار {0} باید {1} کاراکتر باشد.
        /// </summary>
        public static string StringLengthError {
            get {
                return ResourceManager.GetString("StringLengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to مقدار {0} باید منحصر به فرد باشد.
        /// </summary>
        public static string UniqueError {
            get {
                return ResourceManager.GetString("UniqueError", resourceCulture);
            }
        }
    }
}
