using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.Utilities;

namespace LQCE.SharePoint.Layouts.Prestaciones
{
    public partial class EditarRegistros : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeParams();

            // Opcionalmente, agregue código aquí para rellenar previamente los campos del formulario.
        }

        // Se llama a este método cuando el usuario hace clic en el botón para iniciar el flujo de trabajo.
        private string GetInitiationData()
        {
            // TODO: devolver una cadena que contenta los datos de iniciación que se pasarán al flujo de trabajo. Suele estar en formato XML.
            return string.Empty;
        }

        protected void StartWorkflow_Click(object sender, EventArgs e)
        {
            // Opcionalmente, agregue código aquí para realizar pasos adicionales antes de iniciar el flujo de trabajo.
            try
            {
                HandleStartWorkflow();
            }
            catch (Exception)
            {
                SPUtility.TransferToErrorPage(SPHttpUtility.UrlKeyValueEncode("No se pudo iniciar el flujo de trabajo"));
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect("Workflow.aspx", SPRedirectFlags.RelativeToLayoutsPage, HttpContext.Current, Page.ClientQueryString);
        }

        #region Código de iniciación de flujo de trabajo: normalmente no debería modificarse el código siguiente

        private string associationGuid;
        private SPList workflowList;
        private SPListItem workflowListItem;

        private void InitializeParams()
        {
            try
            {
                this.associationGuid = Request.Params["TemplateID"];

                // Los parámetros 'List' e 'ID' serán null para los flujos de trabajo del sitio.
                if (!String.IsNullOrEmpty(Request.Params["List"]) && !String.IsNullOrEmpty(Request.Params["ID"]))
                {
                    this.workflowList = this.Web.Lists[new Guid(Request.Params["List"])];
                    this.workflowListItem = this.workflowList.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                }
            }
            catch (Exception)
            {
                SPUtility.TransferToErrorPage(SPHttpUtility.UrlKeyValueEncode("No se pudieron leer los parámetros de la solicitud"));
            }
        }

        private void HandleStartWorkflow()
        {
            if (this.workflowList != null && this.workflowListItem != null)
            {
                StartListWorkflow();
            }
            else
            {
                StartSiteWorkflow();
            }
        }

        private void StartSiteWorkflow()
        {
            SPWorkflowAssociation association = this.Web.WorkflowAssociations[new Guid(this.associationGuid)];
            this.Web.Site.WorkflowManager.StartWorkflow((object)null, association, GetInitiationData(), SPWorkflowRunOptions.Synchronous);
            SPUtility.Redirect(this.Web.Url, SPRedirectFlags.UseSource, HttpContext.Current);
        }

        private void StartListWorkflow()
        {
            SPWorkflowAssociation association = this.workflowList.WorkflowAssociations[new Guid(this.associationGuid)];
            this.Web.Site.WorkflowManager.StartWorkflow(workflowListItem, association, GetInitiationData());
            SPUtility.Redirect(this.workflowList.DefaultViewUrl, SPRedirectFlags.UseSource, HttpContext.Current);
        }
        #endregion
    }
}