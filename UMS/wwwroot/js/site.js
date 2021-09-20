

const body = document.getElementsByTagName('body')[0]

function collapseSidebar() {
	body.classList.toggle('sidebar-expand')
}

window.onclick = function (event) {
	openCloseDropdown(event)
}

function closeAllDropdown() {
	var dropdowns = document.getElementsByClassName('dropdown-expand-nav')
	for (var i = 0; i < dropdowns.length; i++) {
		dropdowns[i].classList.remove('dropdown-expand-nav')
	}
}

function openCloseDropdown(event) {
	if (!event.target.matches('.dropdown-toggle-nav')) {
		// 
		// Close dropdown when click out of dropdown menu
		// 
		closeAllDropdown()
	} else {
		var toggle = event.target.dataset.toggle
		var content = document.getElementById(toggle)
		if (content.classList.contains('dropdown-expand-nav')) {
			closeAllDropdown()
		} else {
			closeAllDropdown()
			content.classList.add('dropdown-expand-nav')
		}
	}
}
