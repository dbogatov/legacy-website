export module Utility {
	export class Utility {
		public static getMonthName(month: number): string {
			var monthNames = ["January", "February", "March", "April", "May", "June",
				"July", "August", "September", "October", "November", "December"
			];
			return monthNames[month];
		}
	}

	export interface ISerializable<T> {
		deserialize(input: Object): T;
	}
	
	export interface ITemplatable {
		getHtmlView(): string;
	}
}	