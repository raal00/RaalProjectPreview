Vue.component("user-modal", {
	template: '<template v-if="adminPanelVue.showModalUser"><transition name="modal"><div class="modal-mask"><div class="modal-wrapper"><div class="modal-container"><div class="modal-header"><slot name="header"><h5 class="modal-title text-center">Server response</h5><button type="button" class="close" v-on:click="Close()" aria-label="Close"><span aria-hidden="true">&times;</span></button></slot></div><div class="modal-body"><slot name="body"><label for="userModal_Name">Имя</label><input id="userModal_Name" class="form-control" v-model="user.Name" /><label for="userModal_Code">Код</label><input id="userModal_Code" class="form-control" v-model="user.Code" /><label for="userModal_Address">Адрес</label><input id="userModal_Address" class="form-control" v-model="user.Address" /><label for="userModal_Discount">Скидка</label><input id="userModal_Discount" class="form-control" v-model="user.Discount" /><hr /><label for="userModal_Login">Логин</label><input id="userModal_Login" class="form-control" v-model="user.Login" /><label for="userModal_PasswordHash">Хэш пароля</label><input id="userModal_PasswordHash" class="form-control" v-model="user.PasswordHash" /><hr /><label for="userModal_ClientRole">Роль (0-админ/1-покупатель)</label><input id="userModal_ClientRole" class="form-control" v-model="user.ClientRole" /></slot></div><div class="modal-footer"><slot name="footer"><button class="modal-default-button" v-on:click="CompleteUser()">OK</button></slot></div></div></div></div></transition></template>',
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
	template: '<template v-if="adminPanelVue.showModalItem"><transition name="modal"><div class="modal-mask"><div class="modal-wrapper"><div class="modal-container"><div class="modal-header"><slot name="header"><h5 class="modal-title text-center">Server response</h5><button type="button" class="close" v-on:click="Close()" aria-label="Close"><span aria-hidden="true">&times;</span></button></slot></div><div class="modal-body"><slot name="body"><label for="itemModal_Name">Название</label><input id="itemModal_Name" class="form-control" v-model="item.Name" /><label for="itemModal_Price">Цена</label><input id="itemModal_Price" class="form-control" v-model="item.Price" /><label for="itemModal_Category">Категория</label><input id="itemModal_Category" class="form-control" v-model="item.Category" /></slot></div><div class="modal-footer"><slot name="footer"><button class="modal-default-button" v-on:click="CompleteItem()">OK</button></slot></div></div></div></div></transition></template>',
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
	template: '<template v-if="adminPanelVue.showModalAlert"><transition name="modal"><div class="modal-mask"><div class="modal-wrapper"><div class="modal-container"><div class="modal-header"><slot name="header"><h5 class="modal-title text-center">Server response</h5><button type="button" class="close" v-on:click="Close()" aria-label="Close"><span aria-hidden="true">&times;</span></button></slot></div><div class="modal-body"><slot name="body"><h3 v-bind:class="stl">{{msg}}</h3></slot></div><div class="modal-footer"><slot name="footer"><button class="modal-default-button" v-on:click="Close()">OK</button></slot></div></div></div></div></transition></template>',
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