﻿#pragma checksum "..\..\..\VideoPlayer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2D4D2DDB70D3D98C8F0881847EE59592"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
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
using System.Windows.Forms.Integration;
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


namespace VideoPlayer {
    
    
    /// <summary>
    /// VideoPlayer
    /// </summary>
    public partial class VideoPlayer : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement mediaElement1;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Play;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider Volume;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider TimeLine;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas empty;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas black;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label over;
        
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
            System.Uri resourceLocater = new System.Uri("/Voice;component/videoplayer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\VideoPlayer.xaml"
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
            this.mediaElement1 = ((System.Windows.Controls.MediaElement)(target));
            
            #line 8 "..\..\..\VideoPlayer.xaml"
            this.mediaElement1.MediaOpened += new System.Windows.RoutedEventHandler(this.mediaElement1_MediaOpened);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Play = ((System.Windows.Controls.Button)(target));
            
            #line 9 "..\..\..\VideoPlayer.xaml"
            this.Play.Click += new System.Windows.RoutedEventHandler(this.button_Play);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Volume = ((System.Windows.Controls.Slider)(target));
            
            #line 10 "..\..\..\VideoPlayer.xaml"
            this.Volume.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ChangeMediaVolume);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TimeLine = ((System.Windows.Controls.Slider)(target));
            
            #line 12 "..\..\..\VideoPlayer.xaml"
            this.TimeLine.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.TimeLine_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.empty = ((System.Windows.Controls.Canvas)(target));
            return;
            case 6:
            this.black = ((System.Windows.Controls.Canvas)(target));
            return;
            case 7:
            this.over = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

