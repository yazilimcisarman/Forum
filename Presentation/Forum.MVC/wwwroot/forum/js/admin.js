// Admin Panel JavaScript
document.addEventListener('DOMContentLoaded', () => {
    // DOM Elements
    const adminSidebar = document.querySelector('.admin-sidebar');
    const adminContent = document.querySelector('.admin-content');
    const adminHeader = document.querySelector('.admin-header');
    const adminToggle = document.querySelector('.admin-toggle');
    const logoutBtns = document.querySelectorAll('.logout-btn');

    // Sidebar Toggle Function
    function toggleSidebar() {
        adminSidebar.classList.toggle('show');
        document.body.classList.toggle('sidebar-open');
    }

    // Check Screen Size Function
    function checkScreenSize() {
        const isMobile = window.innerWidth <= 991.98;
        if (isMobile) {
            adminSidebar.classList.remove('show');
            document.body.classList.remove('sidebar-open');
        }
    }

    // Initialize Event Listeners
    function initEventListeners() {
        // Sidebar toggle click event
        adminToggle.addEventListener('click', toggleSidebar);

        // Window resize event with debounce
        let resizeTimer;
        window.addEventListener('resize', () => {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(checkScreenSize, 250);
        });

        // Close sidebar when clicking outside on mobile
        document.addEventListener('click', (event) => {
            const isMobile = window.innerWidth <= 991.98;
            if (isMobile && 
                !adminSidebar.contains(event.target) && 
                !adminToggle.contains(event.target) &&
                adminSidebar.classList.contains('show')) {
                toggleSidebar();
            }
        });

        // Close sidebar when clicking nav links on mobile
        document.querySelectorAll('.admin-sidebar .nav-link').forEach(link => {
            link.addEventListener('click', () => {
                if (window.innerWidth <= 991.98 && adminSidebar.classList.contains('show')) {
                    toggleSidebar();
                }
            });
        });

        // Logout confirmation
        logoutBtns.forEach(btn => {
            btn.addEventListener('click', (e) => {
                e.preventDefault();
                if (confirm('Çıkış yapmak istediğinizden emin misiniz?')) {
                    window.location.href = 'giris.html';
                }
            });
        });
    }

    // Notification System
    window.showNotification = function(message, type = 'success') {
        const notification = document.createElement('div');
        notification.className = `alert alert-${type} fade show position-fixed`;
        notification.style.cssText = 'top: 1rem; right: 1rem; z-index: 1500; animation: fadeIn 0.3s ease-in;';
        notification.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Kapat"></button>
        `;
        document.body.appendChild(notification);

        // Auto remove after 3 seconds
        setTimeout(() => {
            notification.classList.remove('show');
            setTimeout(() => notification.remove(), 300);
        }, 3000);
    }

    // Table Sorting
    function initTableSorting() {
        document.querySelectorAll('table.sortable th').forEach((header, index) => {
            if (!header.classList.contains('no-sort')) {
                header.style.cursor = 'pointer';
                header.addEventListener('click', () => {
                    const table = header.closest('table');
                    const tbody = table.querySelector('tbody');
                    const rows = Array.from(tbody.querySelectorAll('tr'));
                    const isAsc = header.classList.contains('asc');

                    // Update sort indicators
                    header.closest('tr').querySelectorAll('th').forEach(th => {
                        th.classList.remove('asc', 'desc');
                    });
                    header.classList.toggle('desc', isAsc);
                    header.classList.toggle('asc', !isAsc);

                    // Sort rows
                    rows.sort((a, b) => {
                        const aValue = a.children[index].textContent.trim();
                        const bValue = b.children[index].textContent.trim();
                        
                        // Check if values are numbers
                        const aNum = parseFloat(aValue);
                        const bNum = parseFloat(bValue);
                        
                        if (!isNaN(aNum) && !isNaN(bNum)) {
                            return isAsc ? aNum - bNum : bNum - aNum;
                        }
                        
                        return isAsc ? 
                            aValue.localeCompare(bValue, 'tr') : 
                            bValue.localeCompare(aValue, 'tr');
                    });

                    // Reorder table
                    tbody.append(...rows);
                });
            }
        });
    }

    // Form Validation
    function initFormValidation() {
        document.querySelectorAll('form.needs-validation').forEach(form => {
            form.addEventListener('submit', (event) => {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                    showNotification('Lütfen tüm zorunlu alanları doldurun.', 'danger');
                }
                form.classList.add('was-validated');
            });

            // Real-time validation
            form.querySelectorAll('input, select, textarea').forEach(input => {
                input.addEventListener('input', () => {
                    if (input.checkValidity()) {
                        input.classList.remove('is-invalid');
                        input.classList.add('is-valid');
                    } else {
                        input.classList.remove('is-valid');
                        input.classList.add('is-invalid');
                    }
                });
            });
        });
    }

    // Stats Animation
    function initStatsAnimation() {
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add('fade-in');
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });

        document.querySelectorAll('.stats-card').forEach(stat => observer.observe(stat));
    }

    // Initialize Admin Panel
    function initAdminPanel() {
        checkScreenSize();
        initEventListeners();
        initTableSorting();
        initFormValidation();
        initStatsAnimation();
    }

    // Run initialization
    initAdminPanel();
}); 