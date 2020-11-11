Vue.component("user-modal", {
	template: "#usermodal-template",
	props: {
		user: {}
	},
	data: function () {
		
	},
	methods: {
		Close() {
			this.$emit("modal-closed");
		},
		Submit() {
			this.$emit("modal-submit");
		},
		CompleteUser: function () {
			var URL = '';
			this.Submit();
			if (adminPanelVue.editMod) {
				URL = '/Admin/EditUser';
			}
			else {
				URL = '/Admin/AddNewUser';
            }
			dataPost = {
				AllUserData: this.user
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: URL,
				success: function (response) {
						adminPanelVue.ShowAllUsers();
				}
			});
		},
	}
});
Vue.component("item-modal", {
	template: "#itemmodal-template",
	props: {
		item: {}
	},
	data: function () {

	},
	methods: {
		Close() {
			this.$emit("modal-closed");
		},
		Submit() {
			this.$emit("modal-submit");
		},
		CompleteItem: function () {
			var URL = '';
			this.Submit();
			if (adminPanelVue.editMod) {
				URL = '/Admin/EditItemInShop';
			}
			else {
				URL = '/Admin/AddItemToShop';
			}
			dataPost = {
				Item: this.item
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: URL,
				success: function (response) {
					adminPanelVue.ShowAllItems();
				}
			});
		}
	}
});
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