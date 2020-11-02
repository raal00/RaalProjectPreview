
var adminPanelVue = new Vue({
	el: '#adminPanelVue',
	data:
	{
		Users: [],
		Orders: [],
		Items: [],
		NewItem: {
			Id: -1,
			Code: 'XX-XXXX-YYXX',
			Name: '',
			Price: 0,
			Category: ''
		},
		NewUser: {
			Customer: {
				Id: -1,
				Name: '',
				Code: 'XXXX-YYYY',
				Address: '',
				Discount: 0.0
			},
			AuthUserData: {
				Id: -1,
				CustomerId: -1,
				Login: '',
				PasswordHash: ''
			},
			UserRole: {
				Id: -1,
				CustomerId: -1,
				ClientRole: 0
			}
		},
		editMod: false,
		alertClass: '',
		alertMsg: ''
	},
	methods:
	{
		ShowAllUsers: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Admin/ShowUsers",
				success: function (response) {
					vue.Users = response.UserList;
					if (vue.Users.length == 0) {
						vue.alertMsg = 'No users';
						vue.alertClass = 'text-warning';
						$('#alertModal').modal('show');
					}
				}
			});
        },
		ShowAllOrders: function() {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Admin/ShowOrders",
				success: function (response) {
					vue.Orders = response.Orders;
					if (vue.Orders.length == 0) {
						vue.alertMsg = 'No orders';
						vue.alertClass = 'text-warning';
						$('#alertModal').modal('show');
					}
				}
			});
		},
		ShowAllItems: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Admin/ShowItems",
				success: function (response) {
					vue.Items = response.Items;
					if (vue.Items.length == 0) {
						vue.alertMsg = 'No items';
						vue.alertClass = 'text-warning';
						$('#alertModal').modal('show');
					}
				}
			});
		},

		StartAddNewUser: function () {
			var vue = this;
			vue.editMod = false;
			vue.NewUser.Customer.Name = '';
			vue.NewUser.Customer.Code = 'XXXX-YYYY';
			vue.NewUser.Customer.Address = '';
			vue.NewUser.Customer.Discount = 0;

			vue.NewUser.AuthUserData.Login = '';
			vue.NewUser.AuthUserData.PasswordHash = '';

			vue.NewUser.UserRole.ClientRole = 0;
			$('#AddUserModal').modal('show');
		},
		StartEditUser: function (id) {
			var vue = this;
			vue.editMod = true;
			var user = vue.Users[id];
			vue.NewUser.Customer.Id = user.Customer.Id;
			vue.NewUser.Customer.Name = user.Customer.Name;
			vue.NewUser.Customer.Code = user.Customer.Code;
			vue.NewUser.Customer.Address = user.Customer.Address;
			vue.NewUser.Customer.Discount = user.Customer.Discount;

			vue.NewUser.AuthUserData.Login = user.AuthUserData.Login;
			vue.NewUser.AuthUserData.PasswordHash = user.AuthUserData.PasswordHash;

			vue.NewUser.UserRole.ClientRole = user.UserRole.ClientRole;
			$('#AddUserModal').modal('show');
		},
		CompleteUser: function () {
			$('#AddUserModal').modal('hide');
			var vue = this;
			var URL = '';
			if (vue.editMod == true) {
				URL = '/Admin/EditUser';
			}
			else {
				URL = '/Admin/AddNewUser';
            }
			dataPost = {
				AllUserData: vue.NewUser
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: URL,
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						$('#alertModal').modal('show');
                    }
					vue.ShowAllUsers();
				}
			});
		},

		StartAddNewItem: function () {
			var vue = this;
			vue.editMod = false;
			vue.NewItem.Code = 'XX-XXXX-YYXX';
			vue.NewItem.Name = '';
			vue.NewItem.Price = 0;
			vue.NewItem.Category = '';
			$('#AddItemModal').modal('show');
		},
		StartEditItem: function (id) {
			var vue = this;
			vue.editMod = true;
			var item = vue.Items[id];
			vue.NewItem.Id = item.Id;
			vue.NewItem.Code = item.Code;
			vue.NewItem.Name = item.Name;
			vue.NewItem.Price = item.Price;
			vue.NewItem.Category = item.Category;
			$('#AddItemModal').modal('show');
		},
		CompleteItem: function () {
			var vue = this;
			$('#AddItemModal').modal('hide');
			var URL = '';
			if (vue.editMod == true) {
				URL = '/Admin/EditItemInShop';
			}
			else {
				URL = '/Admin/AddItemToShop';
			}
			dataPost = {
				Item: vue.NewItem
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: URL,
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						$('#alertModal').modal('show');
					}
					vue.ShowAllItems();
				}
			});
		},
		
		DeleteUser: function (id) {
			var vue = this;
			var usrid = vue.Users[id].Customer.Id;
			dataPost = {
				UserId: usrid
			};
			console.log(usrid);
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Admin/DeleteUser",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						$('#alertModal').modal('show');
					}
					else {
						vue.Users.splice(id, 1);
					}
				}
			});
		},
		DeleteItem: function (id) {
			var vue = this;
			var indx = vue.Items[id].Id;
			dataPost = {
				ItemId: indx
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Admin/DeleteItemFromShop",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						$('#alertModal').modal('show');
					}
					else {
						vue.Items.splice(id, 1);
					}
				}
			});
		},

		SetCompletedOrderStatus: function (id) {
			var vue = this;
			var indx = vue.Orders[id].Id;
			dataPost = {
				OrderId: indx
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Admin/SetCompletedOrderStatus",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						$('#alertModal').modal('show');
					}
					vue.ShowAllOrders();
				}
			});
        },
		SetInProcessingOrderStatus: function (id) {
			var vue = this;
			var indx = vue.Orders[id].Id;
			dataPost = {
				OrderId: indx
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Admin/SetInProcessingOrderStatus",
				success: function (response) {
					console.log(response);
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						$('#alertModal').modal('show');
					}
					vue.ShowAllOrders();
				}
			});
		}

	}
});