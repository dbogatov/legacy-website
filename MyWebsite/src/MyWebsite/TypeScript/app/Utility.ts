export module Utility {
	export class Utility {
		public static getMonthName(month: number): string {
			var monthNames = ["January", "February", "March", "April", "May", "June",
				"July", "August", "September", "October", "November", "December"
			];
			return monthNames[month];
		}

		public static enableAJAXLoadBar(): void {
			
			var handler = (event) => {
				event.stopPropagation();
				event.preventDefault();
			};
			
			$(document).ajaxStart(() => {
				document.addEventListener("click", handler, true);

				$('#myModal').modal({
					keyboard: false
				});
				$('#myModal').modal('show');
			});
			
			$(document).ajaxStop(() => {
				$('#myModal').modal('hide');
				document.removeEventListener("click", handler, true);
			});
		}
	}

	export interface ISerializable<T> {
		deserialize(input: Object): T;
	}
	
	export interface ITemplatable {
		getHtmlView(): string;
	}
}	