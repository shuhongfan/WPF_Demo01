#pragma checksum "..\..\MeetingRoom.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DA824C832095ED74E5ABDB1D8B202981"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace WcfNetMeeting {
    
    
    /// <summary>
    /// MeetingRoom
    /// </summary>
    public partial class MeetingRoom : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\MeetingRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxUserName;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\MeetingRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEnterRoom;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\MeetingRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExitRoom;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\MeetingRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxMember;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\MeetingRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxTalk;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\MeetingRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSay;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\MeetingRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxTalk;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WcfNetMeeting;component/meetingroom.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MeetingRoom.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.textBoxUserName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.btnEnterRoom = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\MeetingRoom.xaml"
            this.btnEnterRoom.Click += new System.Windows.RoutedEventHandler(this.btnLogin_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnExitRoom = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\MeetingRoom.xaml"
            this.btnExitRoom.Click += new System.Windows.RoutedEventHandler(this.btnExitRoom_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listBoxMember = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.listBoxTalk = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this.btnSay = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\MeetingRoom.xaml"
            this.btnSay.Click += new System.Windows.RoutedEventHandler(this.btnSay_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.textBoxTalk = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\MeetingRoom.xaml"
            this.textBoxTalk.KeyDown += new System.Windows.Input.KeyEventHandler(this.textBoxTalk_KeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

