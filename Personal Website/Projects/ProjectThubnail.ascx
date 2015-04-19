<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectThubnail.ascx.cs" Inherits="Personal_Website.Projects.ProjectThubnail" %>

<div class='col-sm-6 col-md-4 project-thumbnail <%# ProjectType %>' style='padding-top:10px' >
	<div class='thumbnail' style='height: 450px'>
		<img src='<%# ImageSrc %>' alt='Here should have been an image' style='max-height:255px' class='img-rounded'>
		<div class='fixHeight'></div>
		<div class='caption'>
			<h3><%# Title %></h3>
			<h5><%# Date %></h5>
			<p class='description' style='text-align: justify;'><%# Description %></p>
			<p><a href='<%# Personal_Website.Models.Authentication.grantAccess() ? TryRef : "#" %>' target=_blank class='btn btn-primary <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "disabled" %>' role='button'>Try it!</a> <a href='<%# Personal_Website.Models.Authentication.grantAccess() ? SrcRef : "#" %>' target=_blank class='btn btn-default <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "disabled" %>' role='button'>View source</a></p>
		</div>
	</div>
</div>