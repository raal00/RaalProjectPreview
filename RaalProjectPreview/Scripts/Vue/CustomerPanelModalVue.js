Vue.component("alert-modal", {
	template: '<template v-if="customerPanelVue.showModalAlert"><transition name="modal"><div class="modal-mask"><div class="modal-wrapper"><div class="modal-container"><div class="modal-header"><slot name="header"><h5 class="modal-title text-center">Server response</h5><button type="button" class="close" v-on:click="Close()" aria-label="Close"><span aria-hidden="true">&times;</span></button></slot></div><div class="modal-body"><slot name="body"><h3 v-bind:class="stl">{{msg}}</h3></slot></div><div class="modal-footer"><slot name="footer"><button class="modal-default-button" v-on:click="Close()">OK</button></slot></div></div></div></div></transition></template>',
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