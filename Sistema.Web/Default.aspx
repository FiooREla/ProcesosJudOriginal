﻿<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    EnableViewState="False" EnableSessionState="ReadOnly" %>

<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SideHolder" runat="Server">
    <dx:aspxtreeview runat="server" id="MailTree" clientinstancename="ClientMailTree" allowselectnode="True">
        <Nodes>
            <dx:TreeViewNode Text="Reporte Expedientes" Expanded="True" Image-SpriteProperties-CssClass="Sprite_Person">
<Image>
<SpriteProperties CssClass="Sprite_Person"></SpriteProperties>
</Image>
                <Nodes>
                    <dx:TreeViewNode Text="Inbox" Expanded="True" Image-SpriteProperties-CssClass="Sprite_Inbox" Visible="False">
<Image>
<SpriteProperties CssClass="Sprite_Inbox"></SpriteProperties>
</Image>
                        <Nodes>
                            <dx:TreeViewNode Text="ASP" Image-SpriteProperties-CssClass="Sprite_ASP" >
<Image>
<SpriteProperties CssClass="Sprite_ASP"></SpriteProperties>
</Image>
                            </dx:TreeViewNode>
                            <dx:TreeViewNode Text="Announcements" Image-SpriteProperties-CssClass="Sprite_Announcements" >
<Image>
<SpriteProperties CssClass="Sprite_Announcements"></SpriteProperties>
</Image>
                            </dx:TreeViewNode>
                            <dx:TreeViewNode Text="IDE Tools" Image-SpriteProperties-CssClass="Sprite_IDE" >
<Image>
<SpriteProperties CssClass="Sprite_IDE"></SpriteProperties>
</Image>
                            </dx:TreeViewNode>
                            <dx:TreeViewNode Text="Frameworks" Image-SpriteProperties-CssClass="Sprite_Frameworks" >
<Image>
<SpriteProperties CssClass="Sprite_Frameworks"></SpriteProperties>
</Image>
                            </dx:TreeViewNode>
                        </Nodes>
                    </dx:TreeViewNode>                    
                    <dx:TreeViewNode Text="Sent Items" Enabled="false" Image-SpriteProperties-CssClass="Sprite_SentItems" Visible="False" >
<Image>
<SpriteProperties CssClass="Sprite_SentItems"></SpriteProperties>
</Image>
                    </dx:TreeViewNode>
                    <dx:TreeViewNode Text="Drafts" Enabled="false" Image-SpriteProperties-CssClass="Sprite_Drafts" Visible="False" >
<Image>
<SpriteProperties CssClass="Sprite_Drafts"></SpriteProperties>
</Image>
                    </dx:TreeViewNode>
                </Nodes>
            </dx:TreeViewNode>
        </Nodes>
        <Styles>
            <NodeImage Paddings-PaddingTop="3px" />
        </Styles>
    </dx:aspxtreeview>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="Server">
    <dx:aspxcallbackpanel id="MailPanel" runat="server" rendermode="Div" height="100%" clientinstancename="ClientMailPanel">
        <PanelCollection>
            <dx:PanelContent runat="server">                    
                <dx:ASPxSplitter ID="MailSplitter" runat="server" Width="100%" Height="100%" Orientation="Vertical" ClientInstanceName="ClientMailSplitter" SaveStateToCookies="True">                    
                    <Panes>
                        <dx:SplitterPane Size="75%" Name="GridPane">
                            <PaneStyle>
                                <Paddings Padding="0" />
                                <Border BorderWidth="0" />
                            </PaneStyle>
                            <ContentCollection>
                                <dx:SplitterContentControl runat="server">
                                    <dx:ASPxMenu runat="server" ID="MailMenu" RenderMode="Lightweight" ClientInstanceName="ClientMailMenu" ItemAutoWidth="false" Width="100%" ShowAsToolbar="true">
                                        <BorderBottom BorderWidth="0" />
                                        <Items>
                                            <dx:MenuItem Text="Fecha Inicio: ">                                                
                                            </dx:MenuItem>                                                                                    
                                            <dx:MenuItem Text="Fecha Inicio" Name="Fecha1">
                                                <template>                                                    
                                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit>                                                    
                                                </template>
                                            </dx:MenuItem>
                                            <dx:MenuItem Text="Fecha Fin: "></dx:MenuItem>
                                            <dx:MenuItem Text="Fecha Fin" Name="Fecha2">
                                                <template>                                                    
                                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit>                                                    
                                                </template>
                                            </dx:MenuItem>      
                                            <dx:MenuItem Text="Actualizar Datos">
                                                <template>
                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Actualizar Datos">
                                                    </dx:ASPxButton>
                                                </template>
                                            </dx:MenuItem>                                      
                                        </Items>
                                    </dx:ASPxMenu>
                                        <dx:ASPxGridView runat="server" ID="MailGrid" KeyboardSupport="true" AccessKey="G"
                                            ClientInstanceName="ClientMailGrid" Width="100%" KeyFieldName="IdExpediente" EnableRowsCache="false" Caption="Expedientes" OnFocusedRowChanged="MailGrid_FocusedRowChanged" >                                            
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Código" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="Codigo" Width="80px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn Caption="Fecha" VisibleIndex="2" FieldName="FechaInicio" Width="80px">
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn Caption="Demandante" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="NombreDemandante">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Demandado" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="NombreDemandado">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Tipo Proceso" ShowInCustomizationForm="True" VisibleIndex="5" FieldName="DescripcionTipoProceso" Width="120px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Clase Proceso" ShowInCustomizationForm="True" VisibleIndex="6" FieldName="DescripcionClaseProceso">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Descripción" ShowInCustomizationForm="True" VisibleIndex="7" FieldName="Descripcion">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Soles" ShowInCustomizationForm="True" VisibleIndex="8" FieldName="MontoSoles" Width="80px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Dolares" ShowInCustomizationForm="True" VisibleIndex="9" FieldName="MontoDolares" Width="80px">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="true" AllowClientEventsOnLoad="false" 
                                                AutoExpandAllGroups="true" EnableRowHotTrack="True" ColumnResizeMode="NextColumn" />
                                            <SettingsPager Mode="ShowAllRecords" />
                                            <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="0" ShowGroupedColumns="True" GridLines="Vertical" ShowFilterRow="True" />
                                            <Styles>
                                                <Row Cursor="pointer" />
                                            </Styles>
                                            <ClientSideEvents 
                                            Init="Judicial.ClientMailGrid_Init"
                                            FocusedRowChanged="Judicial.ClientMailGrid_FocusedRowChanged"
                                            EndCallback="Judicial.ClientMailGrid_EndCallback"
                                        />
                                        </dx:ASPxGridView>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane Name="DetallePane" ScrollBars="Auto">
                            <panes>
                                <dx:SplitterPane Name="PaneDetalle1">
                                    <panestyle>
                                        <paddings padding="0px" />
                                    </panestyle>
                                    <contentcollection>
                                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="gvDetalles1" runat="server" AutoGenerateColumns="False" 
                                                Caption="Actos procesales" Width="100%"  RenderMode="Div"
                                                ClientInstanceName="GVDetalles1" KeyFieldName="IdActoProCont" OnCustomCallback="gvDetalles1_CustomCallback">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Código" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="ActoProcesal.Codigo" Width="60px">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn Caption="Fecha" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="ActoProcesal.FechaRegistro" Width="80px">
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn Caption="Contenido" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="ActoProcesal.Contenido">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tipo Contenido" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="TipoContenido.Descripcion">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <settingsbehavior allowfocusedrow="True" />
                                                <settingspager visible="False">
                                                </settingspager>
                                            </dx:ASPxGridView>
                                        </dx:SplitterContentControl>
                                    </contentcollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane Name="PaneDetalle2" Size="60%">
                                    <panestyle>
                                        <paddings padding="0px" />
                                    </panestyle>
                                    <contentcollection>
                                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="gvDetalles2" runat="server" AutoGenerateColumns="False" Caption="Instancias" Width="100%" ClientInstanceName="GVDetalles2" KeyFieldName="IdExpedienteInstancia" OnCustomCallback="gvDetalles2_CustomCallback">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Sede Judicial" ShowInCustomizationForm="True" VisibleIndex="0" FieldName="DescripcionSedeJudicial">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Organo Judicial" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="DescripcionOrganoJ">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Descripción Instancia" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="DescripcionInstancia">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Sigla Instancia" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="SiglaInstancia">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn Caption="Inicio" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="FechaInicio" Width="80px">
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataDateColumn Caption="Fin" ShowInCustomizationForm="True" VisibleIndex="5" FieldName="FechaFinal" Width="80px">
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn Caption="Observaciones" ShowInCustomizationForm="True" VisibleIndex="6" FieldName="Observacion">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <settingsbehavior allowfocusedrow="True" />
                                                <settingspager visible="False">
                                                </settingspager>
                                            </dx:ASPxGridView>
                                        </dx:SplitterContentControl>
                                    </contentcollection>
                                </dx:SplitterPane>
                            </panes>                            
<ContentCollection>
<dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True"></dx:SplitterContentControl>
</ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                    <clientsideevents paneresized="Judicial.ClientMailSplitter_PaneResized" />
                </dx:ASPxSplitter>
            </dx:PanelContent>
        </PanelCollection>
        <ClientSideEvents />
    </dx:aspxcallbackpanel>
</asp:Content>
