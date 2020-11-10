var customerPanelVue = new Vue({
	el: '#customerPanelVue',
	data:
	{
		Orders: [],
		Items: [],
		CaseItems: [],
		OnlyNewOrders: false,
		alertClass: '',
		alertMsg: ''
	},
	methods:
	{ 
		GetItemList: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/GetItemList",
				success: function (response) {
					vue.Items = response.Items;
					if (vue.Items.length == 0) {
						vue.alertMsg = 'No items in shop';
						vue.alertClass = 'text-warning';
						$('#alertModal').modal('show');
					}
				}
			});
		},
		AddItemToCase: function (id) {
			var vue = this;
			var indx = vue.Items[id].Id;
			dataPost = {
				ItemId: indx
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Customer/AddItemToCase",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						$('#alertModal').modal('show');
					}
					else {
						vue.GetMyCase();
					}
				}
			});
		},
		CreateOrder: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/CreateOrder",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						$('#alertModal').modal('show');
					}
					else {
						vue.ShowMyOrders();
						vue.GetMyCase();
					}
				}
			});
		},
		ShowMyOrders: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/ShowMyOrders",
				success: function (response) {
					vue.Orders = response.Orders;
					if (response.Orders.length == 0) {
						
						vue.alertMsg = 'No active orders';
						vue.alertClass = 'text-warning';
						$('#alertModal').modal('show');
					}
				}
			});
		},
		CloseOrder: function (id) {
			var vue = this;
			var indx = vue.Orders[id].Id;
			dataPost = {
				OrderId: indx
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Customer/CloseOrder",
				success: function (response) {
					if (response.responseStatus == 2) {
						vue.alertMsg = response.Message;
						vue.alertClass = 'text-danger';
						$('#alertModal').modal('show');
					}
					else {
						vue.ShowMyOrders();
						vue.GetMyCase();
					}
				}
			});
		},
		GetMyCase: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/MyCase",
				success: function (response) {
					if (response.CustomerCase.length == 0) {
						vue.alertMsg = 'No items in case';
						vue.alertClass = 'text-warning';
						$('#alertModal').modal('show');
					}
					vue.CaseItems = response.CustomerCase;
				}
			});
        }
	},
	computed:
	{
		filteredOrderList() {
			var vue = this;
			return vue.Orders.filter(order => {
				if (order.Status == 'New' || vue.OnlyNewOrders == false) return order;
			})
		}
	}
});