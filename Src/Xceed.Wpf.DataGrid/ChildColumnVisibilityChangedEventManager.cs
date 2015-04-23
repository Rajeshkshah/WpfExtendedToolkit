﻿/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Xceed.Wpf.DataGrid
{
  internal class ChildColumnVisibilityChangedEventManager : WeakEventManager
  {
    private ChildColumnVisibilityChangedEventManager()
    {
    }

    internal static void AddListener( ObservableColumnCollection source, IWeakEventListener listener )
    {
      CurrentManager.ProtectedAddListener( source, listener );
    }

    internal static void RemoveListener( ObservableColumnCollection source, IWeakEventListener listener )
    {
      CurrentManager.ProtectedRemoveListener( source, listener );
    }

    protected override void StartListening( object source )
    {
      var columnCollection = ( ObservableColumnCollection )source;
      columnCollection.ColumnVisibilityChanged += new EventHandler( this.OnColumnVisibilityChanged );
    }

    protected override void StopListening( object source )
    {
      var columnCollection = ( ObservableColumnCollection )source;
      columnCollection.ColumnVisibilityChanged -= new EventHandler( this.OnColumnVisibilityChanged );
    }

    private static ChildColumnVisibilityChangedEventManager CurrentManager
    {
      get
      {
        Type managerType = typeof( ChildColumnVisibilityChangedEventManager );
        ChildColumnVisibilityChangedEventManager currentManager = ( ChildColumnVisibilityChangedEventManager )WeakEventManager.GetCurrentManager( managerType );

        if( currentManager == null )
        {
          currentManager = new ChildColumnVisibilityChangedEventManager();
          WeakEventManager.SetCurrentManager( managerType, currentManager );
        }

        return currentManager;
      }
    }

    private void OnColumnVisibilityChanged( object sender, EventArgs args )
    {
      this.DeliverEvent( sender, args );
    }
  }
}
