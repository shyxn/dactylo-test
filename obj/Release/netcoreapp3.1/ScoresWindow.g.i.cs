#pragma checksum "..\..\..\ScoresWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7FC5F27DE6AF41272ABBA8E9486B940714348BA1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using DactyloTest;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace DactyloTest {
    
    
    /// <summary>
    /// ScoresWindow
    /// </summary>
    public partial class ScoresWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Titles;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeNickname;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label nickname;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Titre;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OnlyMyScores;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AllScores;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button QuitScores;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ShowGraphOrTable;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid scoreTable;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DactyloTest.GeneralGraph GeneralGraph;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\ScoresWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DactyloTest.IndividualGraph IndividualGraph;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DactyloTest;V0.0.1.0;component/scoreswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ScoresWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\ScoresWindow.xaml"
            ((DactyloTest.ScoresWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\ScoresWindow.xaml"
            ((DactyloTest.ScoresWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Titles = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.ChangeNickname = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.nickname = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.Titre = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.OnlyMyScores = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\ScoresWindow.xaml"
            this.OnlyMyScores.Click += new System.Windows.RoutedEventHandler(this.FilterBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.AllScores = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\ScoresWindow.xaml"
            this.AllScores.Click += new System.Windows.RoutedEventHandler(this.FilterBtn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.QuitScores = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\ScoresWindow.xaml"
            this.QuitScores.Click += new System.Windows.RoutedEventHandler(this.QuitScores_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ShowGraphOrTable = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\ScoresWindow.xaml"
            this.ShowGraphOrTable.Click += new System.Windows.RoutedEventHandler(this.ShowGraph_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.scoreTable = ((System.Windows.Controls.Grid)(target));
            return;
            case 11:
            this.GeneralGraph = ((DactyloTest.GeneralGraph)(target));
            return;
            case 12:
            this.IndividualGraph = ((DactyloTest.IndividualGraph)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

