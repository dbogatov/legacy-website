<%@ Page Title="My Grades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Grades.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
	<h2><%: Title %>.</h2>

	<ext:ResourceManager ID="ResourceManager" runat="server" />      
        
        <ext:GridPanel 
            ID="GridPanel" 
            runat="server"             
            Title="Grades" 
            Collapsible="false" 
            AnimCollapse="true"
            Icon="Table"
            Width="1000" 
            Height="600"
			Style="margin-left:auto; margin-right:auto">
            <Store>
                <ext:Store ID="Store" runat="server" >
                    <Model>
                        <ext:Model runat="server">
                            <Fields>
								<ext:ModelField Name="Term" Type="Auto" />
                                <ext:ModelField Name="Year" Type="Int" />
                                <ext:ModelField Name="Title" Type="String" />
                                <ext:ModelField Name="CourseID" Type="String" />
                                <ext:ModelField Name="GradePercent" Type="Float" />
								<ext:ModelField Name="Grade" Type="String" />
								<ext:ModelField Name="Status" Type="String" /> 
								
								<ext:ModelField Name="Department" />
                                <ext:ModelField Name="Description" />
							</Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>
            <ColumnModel runat="server">
                <Columns>
                    <ext:Column runat="server" Text="Term" DataIndex="Term" Align="Center" />
                    <ext:Column runat="server" Text="Year" DataIndex="Year" Align="Center" />
					<ext:Column runat="server" Text="Title" DataIndex="Title" Flex="1" />
					<ext:Column runat="server" Text="Course ID" DataIndex="CourseID" Align="Center" />
					<ext:Column runat="server" Text="Grade %" DataIndex="GradePercent" Align="Center" >
						<Renderer Fn="Ext.util.Format.numberRenderer('000.00')" />
					</ext:Column>
					<ext:Column runat="server" Text="Grade" DataIndex="Grade" Align="Center" />
					<ext:Column runat="server" Text="Status" DataIndex="Status" />
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView runat="server" TrackOver="true" />
            </View>
            <SelectionModel>
                <ext:RowSelectionModel runat="server" Mode="Multi" />
            </SelectionModel>
            <Plugins>
                <ext:RowExpander ID="RowExpander" runat="server">
                    <Template ID="Template" runat="server">
                        <Html>
							<p><b>Department:</b> {Department}</p><br/>
							<p><b>Description:</b> {Description}</p>
						</Html>
                    </Template>
                </ext:RowExpander>
            </Plugins>
        </ext:GridPanel>

</asp:Content>
