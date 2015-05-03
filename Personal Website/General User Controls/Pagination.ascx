<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pagination.ascx.cs" Inherits="Personal_Website.General_User_Controls.Pagination" %>


<script>
	$(document).ready(function () {

		var url = $(".urlLabel").text();
		var pageNum = parseInt($(".numLabel").text());
		var actPage = parseInt($(".actLabel").text());
		var dispNum = parseInt($(".dispLabel").text());

		if (dispNum > pageNum) {
			dispNum = pageNum;
		}

		if (actPage >= dispNum) {
			for (var i = dispNum; i > 0; i--) {
				$('<li><a id="page-' + (actPage - dispNum + i) + '" href="' + window.location.pathname + "?page=" + (actPage - dispNum + i) + '">' + (actPage - dispNum + i) + '</a></li>').insertAfter("#prevPageLi");
			}
		} else {
			for (var i = dispNum; i >= 1; i--) {
				$('<li><a id="page-' + i + '" href="' + window.location.pathname + "?page=" + i + '">' + i + '</a></li>').insertAfter("#prevPageLi");
			}
		}
		
		$("#nextPage").attr("href", window.location.pathname + "?page=" + (actPage + 1));
		$("#prevPage").attr("href", window.location.pathname + "?page=" + (actPage - 1));

		if (actPage == 1) {
			$("#prevPageLi").addClass("disabled");
			$("#prevPage").attr("href", "#");
		}

		if (actPage == pageNum) {
			$("#nextPageLi").addClass("disabled");
			$("#nextPage").attr("href", "#");
		}

		$("#page-" + actPage).parent().addClass("active");
		
	});


</script>

<label id="urlLabel" runat="server" hidden class="urlLabel"></label>
<label id="numLabel" runat="server" hidden class="numLabel"></label>
<label id="actLabel" runat="server" hidden class="actLabel"></label>
<label id="dispLabel" runat="server" hidden class="dispLabel"></label>

<nav class="row-centered">
	<ul class="pagination">
		<li id="prevPageLi">
			<a id="prevPage" href="#" aria-label="Previous">
				<span aria-hidden="true">&laquo;</span>
			</a>
		</li>
		
		<li id="nextPageLi">
			<a id="nextPage" href="#" aria-label="Next">
				<span aria-hidden="true">&raquo;</span>
			</a>
		</li>
	</ul>
</nav>