<%@ Page Title="Hlavná stránka" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="icz_03._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Správa projektov firmy</h2>
    </div>

    <p>
        <asp:Label ID="lblState" runat="server" Text="Filtruj podľa názvu projektu:" />
        <asp:DropDownList ID="ddlState" runat="server" 
                 AutoPostBack="true" AppendDataBoundItems="True"
                 OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
        </asp:DropDownList>
    </p>

    <hr />

    <asp:GridView ID="gvProjects" runat="server"
        AutoGenerateColumns="false"
        Width="100%"
        CssClass="grid"
        GridLines="none"
        DataKeyNames="name"
        EmptyDataText = "No record found!"
        EmptyDataRowStyle-CssClass ="gvEmpty">

        <Columns>
          <asp:BoundField HeaderText="Id Projektu" DataField="id" />
          <asp:BoundField HeaderText="Názov Projektu" DataField="name" />  
          <asp:BoundField HeaderText="Skratka Projektu" DataField="abbreviation" />
          <asp:BoundField HeaderText="Zákaznik Projektu" DataField="customer" />

          <asp:TemplateField HeaderText="Akcia">
            <ItemTemplate>
               <asp:ImageButton ID="ibtnEdit" runat="server"
                  ImageUrl="/edit_icon.jpg" ToolTip="upraviť" OnClick="ibtnEdit_Click" />
               <asp:ImageButton ID="ibtnDelete" runat="server"
                  ImageUrl="/delete_icon.jpg" ToolTip="zmazať"
                  OnClientClick="javascript:return confirm('Naozaj chcete vymazať záznam?');" 
                  OnClick="ibtnDelete_Click" />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>

    </asp:GridView>

    <div>
        <asp:Button ID="btnAdd" runat="server" Text="Pridaj projekt"  OnClick="btnAdd_Click"  />
    </div> 

    <asp:Label ID="lblErr" runat="server" />

    <div id="pnlAddPopup" runat="server" style="width:500px; border:1px solid #3d143c; background-color:#ffffff;" >
        <div id="popupheader" class="popuHeader">
            <asp:Label ID="lblHeader" runat="server" Text="Pridať projekt" style="font-weight: bold;" />
            <span style="float:right">
                <img id="imgClose" src="/Images/btn-close.png" alt="close" title="Close" />
            </span>
        </div>

        <div>
            <asp:HiddenField ID="hfProjID" runat="server" Value="0" />
            <asp:HiddenField ID="hfEditID" runat="server" Value="0" />

            <table border="0" class="table-border" >
              <tr>
                <td>Názov projektu</td>
                <td><asp:TextBox ID="txtName" style="width:400px; background-color:#ffffff;" runat="server" /></td>
              </tr>
              <tr>
                <td>Skratka projektu</td>
                <td><asp:TextBox ID="txtAbbreviation" style="width:400px; background-color:#ffffff;" runat="server" /></td>
              </tr>
              <tr>
                <td>Zákazník projektu</td>
                <td><asp:TextBox ID="txtCustomer" style="width:400px; background-color:#ffffff;" runat="server" /></td>
              </tr>
              <tr>
              </tr>
              <tr>
                <td>&nbsp;</td>
                <td>
                   <asp:Button ID="btnSave" runat="server" Text="  Ulož  " 
                      OnClick="btnSave_Click" />
                        &nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnCancel" runat="server" Text=" Späť   "
                OnClientClick="javascript:$find('mpeProjBehavior').hide();return false;" />
                </td>
              </tr>
            </table>
        </div>

        <ajaxToolkit:ModalPopupExtender ID="mpeProj" runat="server" 
            TargetControlID="hfProjID"
            PopupControlID="pnlAddPopup" 
            BehaviorID="mpeProjBehavior"
            DropShadow="true"
            CancelControlID="imgClose" 
            PopupDragHandleControlID="popupheader" />

    </div>

</asp:Content>
