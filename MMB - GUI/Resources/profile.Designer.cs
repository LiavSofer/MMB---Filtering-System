﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MMB_GUI.Resources {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.5.0.0")]
    internal sealed partial class profile : global::System.Configuration.ApplicationSettingsBase {
        
        private static profile defaultInstance = ((profile)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new profile())));
        
        public static profile Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool safe_server {
            get {
                return ((bool)(this["safe_server"]));
            }
            set {
                this["safe_server"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Generic.List<System.String> customBlacklist {
            get {
                return ((global::System.Collections.Generic.List<System.String>)(this["customBlacklist"]));
            }
            set {
                this["customBlacklist"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Generic.List<System.String> exceptionsList {
            get {
                return ((global::System.Collections.Generic.List<System.String>)(this["exceptionsList"]));
            }
            set {
                this["exceptionsList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool system_status {
            get {
                return ((bool)(this["system_status"]));
            }
            set {
                this["system_status"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool first_opening {
            get {
                return ((bool)(this["first_opening"]));
            }
            set {
                this["first_opening"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("@NULL@")]
        public string admin_password {
            get {
                return ((string)(this["admin_password"]));
            }
            set {
                this["admin_password"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string admin_mail {
            get {
                return ((string)(this["admin_mail"]));
            }
            set {
                this["admin_mail"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool scheduelSystem_status {
            get {
                return ((bool)(this["scheduelSystem_status"]));
            }
            set {
                this["scheduelSystem_status"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool strict_search {
            get {
                return ((bool)(this["strict_search"]));
            }
            set {
                this["strict_search"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool social_block {
            get {
                return ((bool)(this["social_block"]));
            }
            set {
                this["social_block"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool gambling_block {
            get {
                return ((bool)(this["gambling_block"]));
            }
            set {
                this["gambling_block"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool news_block {
            get {
                return ((bool)(this["news_block"]));
            }
            set {
                this["news_block"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ad_block {
            get {
                return ((bool)(this["ad_block"]));
            }
            set {
                this["ad_block"] = value;
            }
        }
    }
}
