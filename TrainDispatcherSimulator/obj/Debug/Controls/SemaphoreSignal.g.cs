﻿#pragma checksum "..\..\..\Controls\SemaphoreSignal.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A619CFE83CAE81076002C0ADD6D26F1C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using TrainDispatcherSimulator.Converters;


namespace TrainDispatcherSimulator.Controls {
    
    
    /// <summary>
    /// SemaphoreSignal
    /// </summary>
    public partial class SemaphoreSignal : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\..\Controls\SemaphoreSignal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup semaphorePopup;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\Controls\SemaphoreSignal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse upperSignal;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\Controls\SemaphoreSignal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse lowerSignal;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\Controls\SemaphoreSignal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup manueuvreSemaphorePopup;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\Controls\SemaphoreSignal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle manoeuvreSignal;
        
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
            System.Uri resourceLocater = new System.Uri("/TrainDispatcherSimulator;component/controls/semaphoresignal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\SemaphoreSignal.xaml"
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
            
            #line 42 "..\..\..\Controls\SemaphoreSignal.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.UserControl_MouseUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.semaphorePopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 3:
            
            #line 56 "..\..\..\Controls\SemaphoreSignal.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.redSignal_MouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 60 "..\..\..\Controls\SemaphoreSignal.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.yellowSignal_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 64 "..\..\..\Controls\SemaphoreSignal.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.yellowYellowSignal_MouseUp);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 68 "..\..\..\Controls\SemaphoreSignal.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.greenSignal_MouseUp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.upperSignal = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 8:
            this.lowerSignal = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 9:
            
            #line 109 "..\..\..\Controls\SemaphoreSignal.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.manueuvrePopup_MouseUp);
            
            #line default
            #line hidden
            return;
            case 10:
            this.manueuvreSemaphorePopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 11:
            
            #line 123 "..\..\..\Controls\SemaphoreSignal.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.notActiveSignal_MouseUp);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 134 "..\..\..\Controls\SemaphoreSignal.xaml"
            ((System.Windows.Controls.StackPanel)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.activeSignal_MouseUp);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 160 "..\..\..\Controls\SemaphoreSignal.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.manueuvrePopup_MouseUp);
            
            #line default
            #line hidden
            return;
            case 14:
            this.manoeuvreSignal = ((System.Windows.Shapes.Rectangle)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

