using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LQCE.SharePoint.ControlTemplates.App_Code;
using LQCE.Transaccion.DTO;

namespace LQCE.SharePoint.ControlTemplates.Prestaciones
{
    public delegate void PageChangedEventHandler(object sender, CustomPageChangeArgs e);

    public partial class Paginador : UserControl
    {
        public event PageChangedEventHandler PageChanged;

        private int _currentPageNumber;
        public int CurrentPageNumber
        {
            get { return _currentPageNumber; }
            set { _currentPageNumber = value; }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { if (value < 1) value = 1; _totalPages = value; }
        }

        private int _currentPageSize;
        public int CurrentPageSize
        {
            get { return _currentPageSize; }
            set { _currentPageSize = value; }
        }

        public void Inicializar(DTOPaginador dto)
        {

            ddlPageSize.SelectedValue = dto.PageSize.ToString();

            CustomPageChangeArgs args = new CustomPageChangeArgs();
            args.CurrentPageSize = dto.PageSize;
            args.CurrentPageNumber = dto.PageIndex;
            args.TotalPages = 0;
            Pager_PageChanged(this, args);

            ddlPageNumber.Items.Clear();
            for (int count = 1; count <= this.TotalPages; ++count)
                ddlPageNumber.Items.Add(count.ToString());
            ddlPageNumber.SelectedValue = dto.PageIndex.ToString();
            lblShowRecords.Text = string.Format(" {0} ", this.TotalPages.ToString());

        }

        public void SetPage(int PageIndex)
        {
            ddlPageNumber.Items.Clear();
            for (int count = 1; count <= this.TotalPages; ++count)
                ddlPageNumber.Items.Add(count.ToString());
            ddlPageNumber.SelectedValue = PageIndex.ToString();
            lblShowRecords.Text = string.Format(" {0} ", this.TotalPages.ToString());
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {

            CustomPageChangeArgs args = new CustomPageChangeArgs();
            args.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value);
            args.CurrentPageNumber = 1;
            args.TotalPages = Convert.ToInt32(this.lblShowRecords.Text);
            Pager_PageChanged(this, args);

            ddlPageNumber.Items.Clear();
            for (int count = 1; count <= this.TotalPages; ++count)
                ddlPageNumber.Items.Add(count.ToString());
            ddlPageNumber.Items[0].Selected = true;
            lblShowRecords.Text = string.Format(" {0} ", this.TotalPages.ToString());

        }

        void Pager_PageChanged(object sender, CustomPageChangeArgs e)
        {
            PageChanged(this, e);
            //throw new Exception("The method or operation is not implemented.");
        }

        protected void ddlPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomPageChangeArgs args = new CustomPageChangeArgs();
            args.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value);
            args.CurrentPageNumber = Convert.ToInt32(this.ddlPageNumber.SelectedItem.Text);
            args.TotalPages = Convert.ToInt32(this.lblShowRecords.Text);
            Pager_PageChanged(this, args);

            lblShowRecords.Text = string.Format(" {0} ", args.TotalPages.ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void linkAnterior_Click(object sender, EventArgs e)
        {
            int PaginaActual = Convert.ToInt32(this.ddlPageNumber.SelectedItem.Text);
            if (PaginaActual > 1)
            {
                CustomPageChangeArgs args = new CustomPageChangeArgs();
                args.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value);
                args.CurrentPageNumber = PaginaActual - 1;
                args.TotalPages = Convert.ToInt32(this.lblShowRecords.Text);
                Pager_PageChanged(this, args);

                this.ddlPageNumber.SelectedValue = args.CurrentPageNumber.ToString();
                lblShowRecords.Text = string.Format(" {0} ", args.TotalPages.ToString());
            }
        }

        protected void linkSiguiente_Click(object sender, EventArgs e)
        {
            int PaginaActual = Convert.ToInt32(this.ddlPageNumber.SelectedItem.Text);
            if (PaginaActual < Convert.ToInt32(this.lblShowRecords.Text))
            {
                CustomPageChangeArgs args = new CustomPageChangeArgs();
                args.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value);
                args.CurrentPageNumber = PaginaActual + 1;
                args.TotalPages = Convert.ToInt32(this.lblShowRecords.Text);
                Pager_PageChanged(this, args);

                this.ddlPageNumber.SelectedValue = args.CurrentPageNumber.ToString();
                lblShowRecords.Text = string.Format(" {0} ", args.TotalPages.ToString());
            }
        }
    }
}
