////$(window).on('load', function () {
////	$(".loader").fadeOut(1000);
////	$(".wrapper").fadeIn(1000)
////});


document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.sidebar .sidebar-nav-link').forEach(function (element) {
        element.addEventListener('click', function (e) {
            let nextEl = element.nextElementSibling;
            let parentEl = element.parentElement;

            if (nextEl) {
                e.preventDefault();
                let mycollapse = new bootstrap.Collapse(nextEl);

                if (nextEl.classList.contains('show')) {
                    mycollapse.hide();
                } else {
                    mycollapse.show();
                    // find other submenus with class=show
                    var opened_submenu = parentEl.parentElement.querySelector('.submenu.show');
                    // if it exists, then close all of them
                    if (opened_submenu) {
                        new bootstrap.Collapse(opened_submenu);
                    }
                }
            }
        }); // addEventListener
    }) // forEach
});


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

