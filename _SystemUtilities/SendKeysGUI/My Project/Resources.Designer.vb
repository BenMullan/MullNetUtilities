﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Public Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Public ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("SendKeysGUI.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Public Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;html&gt;
        '''	&lt;head&gt;
        '''		&lt;title&gt;Expansions HTML&lt;/title&gt;
        '''		&lt;style&gt;
        '''			body { padding: 1% 3% 1% 3%; color: #454545; background-color: #EDFEFF; font-family: verdana; }
        '''			table, th, td { border: 1px solid black; }
        '''		&lt;/style&gt;
        '''	&lt;/head&gt;
        '''	&lt;body&gt;
        '''		&lt;center&gt;
        '''			&lt;h1&gt;SendKeys Examples&lt;/h1&gt;
        '''		&lt;/center&gt;
        '''		
        '''			&lt;p&gt;
        '''				&lt;ul&gt;
        '''					&lt;li&gt;Hello World&lt;/li&gt;
        '''					&lt;li&gt;{Enter}&lt;/li&gt;
        '''					&lt;li&gt;{F5}&lt;/li&gt;
        '''					&lt;li&gt;^{v}&amp;emsp;&lt;b&gt;Ctrl + V&lt;/b&gt;&lt;/li&gt;
        '''					&lt;li&gt;^+{F1}&amp;emsp;&lt;b&gt;Ctrl + Shift + F1&lt;/b&gt;&lt;/li&gt;
        '''					&lt;li&gt;+(EC)&amp;emsp;&lt;b&gt;Holds do [rest of string was truncated]&quot;;.
        '''</summary>
        Public ReadOnly Property ExpansionsHTML() As String
            Get
                Return ResourceManager.GetString("ExpansionsHTML", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Drawing.Bitmap.
        '''</summary>
        Public ReadOnly Property ProcExp_WindowSelection_TargetIcon() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ProcExp_WindowSelection_TargetIcon", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
    End Module
End Namespace
