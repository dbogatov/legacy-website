class Main {
    runString: string;

    constructor() {
        this.runString = "hello from main MAC";
    }

    run() {

		$.get("api/Projects", {}, (data) => {
			console.log(data);
		});

    }
}
export = Main;