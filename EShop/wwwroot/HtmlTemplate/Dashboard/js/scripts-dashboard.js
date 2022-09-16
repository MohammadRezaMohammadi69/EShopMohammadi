if (document.getElementById('navbarDash') != null) {
    var navbarDash = document.getElementById('navbarDash');
    var sidebarDash = document.getElementById('sidebarDash');
    var contentDash = document.getElementById('contentDash');
    var sidebarCollapse = document.getElementById('sidebarCollapse');
    var sidebarOverlay = document.querySelector('.sidebar-overlay');
    var btnCloseSidebar = document.querySelector('.btn-close-sidebar');

    sidebarCollapse.onclick = () => {
        navbarDash.classList.toggle('active')
        sidebarDash.classList.toggle('active')
        contentDash.classList.toggle('active')
        sidebarCollapse.classList.toggle('active');
        sidebarOverlay.style.display = "block"
    }
    sidebarOverlay.onclick = () => {
        sidebarOverlay.style.display = "none"
        navbarDash.classList.remove('active')
        sidebarDash.classList.remove('active')
        contentDash.classList.remove('active')
    }
    btnCloseSidebar.onclick = () => {
        sidebarOverlay.style.display = "none"
        navbarDash.classList.remove('active')
        sidebarDash.classList.remove('active')
        contentDash.classList.remove('active')
    }
}

if (document.querySelector('.btn-show-filter') != null) {
    var btnShowFilter = document.querySelector('.btn-show-filter');
    var searchContent = document.querySelector('.search-content');
    btnShowFilter.onclick = () => {
        searchContent.classList.toggle('active')
    }
}

// Add new Slider Inputs
if (document.querySelector('.forms-add-new-slider') != null) {
    var sliderInputCode = document.querySelector('.slider-input-code');
    var sliderInputCategory = document.querySelector('.slider-input-category');
    var sliderInputLink = document.querySelector('.slider-input-link');
}

function selectChangeValue() {
    var selectValue = document.getElementById("selectChooseType").value;
    if (selectValue === "1") {
        sliderInputCode.style.display = "block";
        sliderInputCategory.style.display = "none";
        sliderInputLink.style.display = "none";
    } else if (selectValue === "2") {
        sliderInputCode.style.display = "none";
        sliderInputCategory.style.display = "block";
        sliderInputLink.style.display = "none";
    } else if (selectValue === "3") {
        sliderInputCode.style.display = "none";
        sliderInputCategory.style.display = "none";
        sliderInputLink.style.display = "block";
    }
}