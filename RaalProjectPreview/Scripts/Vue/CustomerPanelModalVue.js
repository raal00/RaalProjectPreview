Vue.component("alert-modal", {
	template: "#alertmodal-template",
	props: {
		msg: '',
		stl: ''
	},
	data: function () {

	},
	methods: {
		Close() {
			this.$emit("modal-closed");
		},
		Submit() {
			this.$emit("modal-submit");
		}
	}
});