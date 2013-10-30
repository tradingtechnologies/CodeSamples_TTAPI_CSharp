using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

namespace TTAPI_Samples
{
    public delegate string GetKeyHandler(Object obj);

    public interface IGridBoundComponent
    {
        void SetPropertyVisibilty(string propertyName, bool visible);
        Dictionary<string, bool> GetAvailableProperties();
    };

    public class GridBoundComponent<T> : IGridBoundComponent
    {
        public GridBoundComponent(DataGridView dataGrid, GetKeyHandler getKeyHandler)
        {
            m_theType = typeof(T); 
            m_dataGrid = dataGrid;
            m_getKeyHandler = getKeyHandler;
            m_dataGrid.AutoGenerateColumns = false;
            m_dataGrid.RowHeadersVisible = true;
            m_dataGrid.RowsAdded += new DataGridViewRowsAddedEventHandler(m_dataGrid_RowsAdded);
            m_dataGrid.DataSource = m_objectList;
            GetProperties();
            BuildHeaderRow();
        }

        void m_dataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            m_dataGrid.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        public void Clear()
        {
            m_objectList.Clear();
        }

        public object GetItem(int index)
        {
            if (m_objectList.Count <= 0)
            {
                return null;
            }

            if ((index < 0) || (index > (m_objectList.Count - 1)))
            {
                return null;
            }

            return m_objectList[index];

        }

        public void Add(T obj)
        {
            string key = m_getKeyHandler(obj);
            m_objectList.Add(obj);
        }

        public void Remove(T obj)
        {
            string key = m_getKeyHandler(obj);
            for (int i = 0; i < m_objectList.Count; i++)
            {
                if (key == m_getKeyHandler(m_objectList[i]))
                {
                    m_objectList.RemoveAt(i);
                    return;
                }
            }
        }

        public int indexOf(string key)
        {
            for (int i = 0; i < m_objectList.Count; i++)
            {
                if (key == m_getKeyHandler(m_objectList[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public void UpdateOrAdd(T obj)
        {

            string key = m_getKeyHandler(obj);
            for (int i = 0; i < m_objectList.Count; i++)
            {
                if (key == m_getKeyHandler(m_objectList[i]))
                {
                    m_objectList[i] = obj;
                    return;
                }
            }

            m_objectList.Add(obj);
        }

        public void Update(T obj)
        {
            string key = m_getKeyHandler(obj);
            for (int i = 0; i < m_objectList.Count; i++)
            {
                if (key == m_getKeyHandler(m_objectList[i]))
                {
                    m_objectList[i] = obj;
                    return;
                }
            }

        }

        private void GetProperties()
        {
            PropertyInfo[] propertyInfos = m_theType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //initially all columns will be visible
                m_availableProperties.Add(propertyInfo.Name, true);
            }
        }

        Dictionary<string, bool> IGridBoundComponent.GetAvailableProperties()
        {
            return m_availableProperties;
        }


        void toolStripMenuItemHideColumn_Click(object sender, EventArgs e)
        {
            doSetPropertyVisibilty(m_dataGrid.Columns[m_dataGrid.CurrentCell.ColumnIndex].Name, false);
        }

        void doSetPropertyVisibilty(string propertyName, bool visible)
        {
            if (m_availableProperties.ContainsKey(propertyName))
            {
                if (m_dataGrid.Columns.Contains(propertyName))
                {
                    m_availableProperties[propertyName] = visible;
                    m_dataGrid.Columns[propertyName].Visible = visible;
                }
            }
        }

        void IGridBoundComponent.SetPropertyVisibilty(string propertyName, bool visible)
        {
            doSetPropertyVisibilty(propertyName, visible);
        }

        public void SetVisibleProperties(IList<string> propertyList)
        {
            foreach (DataGridViewColumn column in m_dataGrid.Columns)
            {
                if (propertyList.Contains(column.DataPropertyName))
                {
                    column.Visible = true;
                    m_availableProperties[column.DataPropertyName] = true; //set visible to true
                }
                else
                {
                    column.Visible = false;
                    m_availableProperties[column.DataPropertyName] = false; //set visible to false
                }
            }
        }

        private void BuildHeaderRow()
        {
            foreach (KeyValuePair<string, bool> kv in m_availableProperties)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = kv.Key;
                column.Visible = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DataPropertyName = kv.Key;
                column.Name = kv.Key;

                m_dataGrid.Columns.Add(column);

            }
        }

        private Type m_theType;
        private DataGridView m_dataGrid;
        public Dictionary<string, bool> m_availableProperties = new Dictionary<string, bool>();
        private BindingList<T> m_objectList = new BindingList<T>();
        private Dictionary<string, int> m_objectDict = new Dictionary<string, int>();
        private GetKeyHandler m_getKeyHandler;
    };
}
