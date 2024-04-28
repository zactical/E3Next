﻿using ComponentFactory.Krypton.Toolkit;
using E3Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace E3NextConfigEditor
{
	public partial class AddSpellEditor : Form
	{
		Dictionary<string, Dictionary<string, List<SpellData>>> _spellDataOrganized;
		List<Bitmap> _spellIcons;
		public AddSpellEditor(Dictionary<string, Dictionary<string, List<SpellData>>> spellDataOrganized, List<Bitmap> spellIcons)
		{
			_spellDataOrganized = spellDataOrganized;
			_spellIcons = spellIcons;
			InitializeComponent();

			PopulateData();
		}
		public void PopulateData()
		{
			var imageList = new ImageList();
			imageList.Images.AddRange(_spellIcons.ToArray());
			spellTreeView.ImageList = imageList;
			foreach (var pair in _spellDataOrganized)
			{
				string cat = pair.Key;
				KryptonTreeNode item = new KryptonTreeNode();
				item.Text = cat;
				

				spellTreeView.Nodes.Add(item);

				foreach(var pair2 in pair.Value)
				{
					string subcat = pair2.Key;
					KryptonTreeNode item2 = new KryptonTreeNode();
					item2.Text = subcat;
					item.Nodes.Add(item2);
					foreach (var spell in pair2.Value)
					{
						if (item.ImageIndex <= 0)
						{
							item.ImageIndex = spell.SpellIcon;
							item.SelectedImageIndex = spell.SpellIcon;
						}
						if (item2.ImageIndex <= 0)
						{
							item2.ImageIndex = spell.SpellIcon;
							item2.SelectedImageIndex = spell.SpellIcon;
						}

						KryptonTreeNode item3 = new KryptonTreeNode();
						item3.Text = spell.SpellName;
						item3.ImageIndex = spell.SpellIcon;
						item3.SelectedImageIndex = spell.SpellIcon;
						item3.Tag = spell;
						item2.Nodes.Add(item3);
					}
				}
			}
		}
		//private void updatePropertyGrid()
		//{
		//	propertyGrid.SelectedObject = null;
		//	//need to pull out the Tag and verify which type so we know what to pass to the 
		//	//property grid
		//	KryptonListItem listItem = ((KryptonListItem)valuesListBox.SelectedItem);
		//	if (listItem.Tag is Spell)
		//	{

		//		propertyGrid.SelectedObject = new Models.SpellDataProxy((Spell)listItem.Tag);

		//	}
		//	else if (listItem.Tag is SpellRequest)
		//	{
		//		propertyGrid.SelectedObject = new Models.SpellRequestDataProxy((SpellRequest)listItem.Tag);
		//	}
		//	else if (listItem.Tag is Models.Ref<string> || listItem.Tag is Models.Ref<bool> || listItem.Tag is Models.Ref<Int32> || listItem.Tag is Models.Ref<Int64>)
		//	{

		//		propertyGrid.SelectedObject = listItem.Tag;

		//	}

		//}
		private void spellTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if(spellTreeView.SelectedNode != null )
			{
				if(spellTreeView.SelectedNode.Tag != null)
				{
					if(spellTreeView.SelectedNode.Tag is SpellData)
					{
						addSpellPropertyGrid.SelectedObject = new Models.SpellDataProxy((SpellData)spellTreeView.SelectedNode.Tag);
					}
				}
			}
		}
	}
}
