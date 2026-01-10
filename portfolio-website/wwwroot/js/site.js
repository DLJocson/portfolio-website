// --- Mobile Menu Toggle ---
function toggleMobileMenu() {
    document.getElementById('mobile-menu').classList.toggle('hidden');
}

// --- Theme Toggle ---
function toggleTheme() {
    if (document.documentElement.classList.contains('dark')) {
        document.documentElement.classList.remove('dark');
        localStorage.theme = 'light';
    } else {
        document.documentElement.classList.add('dark');
        localStorage.theme = 'dark';
    }
    lucide.createIcons();
}

// --- Modal System ---
const modalOverlay = document.getElementById('modal-overlay');
const modalContainer = document.getElementById('modal-container');
const modalTitle = document.getElementById('modal-title');
const modalBody = document.getElementById('modal-body');

// Click Outside to Close
if (modalOverlay) {
    modalOverlay.addEventListener('click', (e) => {
        if (e.target === modalOverlay) closeModal();
    });
}

function showModal(title, contentHTML) {
    modalTitle.textContent = title;
    modalBody.innerHTML = contentHTML;
    
    modalOverlay.classList.remove('hidden');
    setTimeout(() => {
        modalContainer.classList.remove('scale-95');
        modalContainer.classList.add('scale-100');
    }, 10);
    
    modalContainer.focus();
    lucide.createIcons();
}

function closeModal() {
    modalContainer.classList.remove('scale-100');
    modalContainer.classList.add('scale-95');
    setTimeout(() => {
        modalOverlay.classList.add('hidden');
    }, 300);
}

// Close on Escape
document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape' && modalOverlay && !modalOverlay.classList.contains('hidden')) {
        closeModal();
    }
});

// --- Resume Download ---
function initiateDownload() {
    showModal('Downloading...', '<p class="text-center">Preparing your resume PDF. Please wait...</p><div class="w-full bg-slate-200 dark:bg-slate-700 h-2 mt-4 rounded-full overflow-hidden"><div class="bg-purple-vibrant h-full w-1/2 animate-pulse"></div></div>');
    
    setTimeout(() => {
        // Make AJAX call to download endpoint
        fetch('/Home/DownloadResume')
            .then(response => {
                if (response.ok) {
                    return response.blob();
                }
                throw new Error('Download failed');
            })
            .then(blob => {
                const url = window.URL.createObjectURL(blob);
                const a = document.createElement('a');
                a.href = url;
                a.download = 'Dan_Resume.pdf';
                document.body.appendChild(a);
                a.click();
                window.URL.revokeObjectURL(url);
                document.body.removeChild(a);

                showModal('Success!', '<div class="text-center text-green-600 dark:text-green-400 mb-2"><i data-lucide="check-circle" class="w-12 h-12 mx-auto"></i></div><p class="text-center font-bold">Resume Downloaded Successfully.</p>');
                lucide.createIcons();
                setTimeout(closeModal, 2000);
            })
            .catch(error => {
                showModal('Error', '<p class="text-center text-red-500 font-bold">Download failed. Please try again.</p>');
                setTimeout(closeModal, 2000);
            });
    }, 1500);
}

// --- Tech Info ---
function showTechInfo(key) {
    fetch(`/Home/GetTechInfo?id=${key}`)
        .then(response => response.json())
        .then(data => {
            if (data) {
                showModal(data.title, `<p class="leading-relaxed text-lg">${data.desc}</p>`);
            }
        })
        .catch(error => {
            console.error('Error fetching tech info:', error);
        });
}

// --- Project Details ---
function showProject(key) {
    fetch(`/Home/GetProjectDetails?id=${key}`)
        .then(response => response.json())
        .then(data => {
            if (data) {
                const tags = data.technologies.map(t => 
                    `<span class="px-2 py-1 bg-purple-50 dark:bg-slate-800 text-purple-deep dark:text-purple-200 text-xs font-bold rounded-md border border-purple-100 dark:border-slate-700">${t}</span>`
                ).join('');
                
                const html = `
                    <p class="mb-6 leading-relaxed">${data.description}</p>
                    <h4 class="font-bold text-slate-900 dark:text-white mb-2">Technologies Used:</h4>
                    <div class="flex flex-wrap gap-2 mb-6">${tags}</div>
                    <div class="flex gap-4">
                        <button onclick="launchLiveDemo(this)" class="flex-1 py-3 bg-purple-vibrant text-white rounded-lg font-bold text-center hover:bg-purple-deep transition-colors flex items-center justify-center gap-2">
                            <i data-lucide="rocket" class="w-4 h-4"></i> Live Demo
                        </button>
                        <button onclick="openRepo()" class="flex-1 py-3 border border-slate-200 dark:border-slate-700 text-slate-700 dark:text-slate-300 rounded-lg font-bold text-center hover:bg-slate-50 dark:hover:bg-slate-800 transition-colors flex items-center justify-center gap-2">
                            <i data-lucide="code-2" class="w-4 h-4"></i> View Code
                        </button>
                    </div>
                `;
                showModal(data.title, html);
            }
        })
        .catch(error => {
            console.error('Error fetching project details:', error);
        });
}

function launchLiveDemo(btn) {
    const originalContent = btn.innerHTML;
    btn.innerHTML = `<i data-lucide="loader-2" class="w-4 h-4 animate-spin"></i> Launching...`;
    lucide.createIcons();
    setTimeout(() => {
        btn.innerHTML = originalContent;
        lucide.createIcons();
    }, 1500);
}

function openRepo() {
    closeModal();
    setTimeout(() => {
        showModal('GitHub', '<div class="text-center py-4"><i data-lucide="github" class="w-12 h-12 mx-auto mb-2"></i><p class="font-bold">Repository opened successfully.</p></div>');
        lucide.createIcons();
        setTimeout(closeModal, 1500);
    }, 300);
}

// --- Contact Form Submission ---
function submitContactForm() {
    const form = document.getElementById('contact-form');
    const formData = new FormData(form);
    
    showModal('Sending...', '<p class="text-center">Sending your message...</p>');
    
    fetch('/Home/SubmitContact', {
        method: 'POST',
        body: formData
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            showModal('Message Sent', 
                `<div class="text-center text-green-600 dark:text-green-400 mb-2">
                    <i data-lucide="mail-check" class="w-12 h-12 mx-auto"></i>
                </div>
                <p class="text-center">${data.message}</p>`
            );
            lucide.createIcons();
            form.reset();
        } else {
            showModal('Error', `<p class="text-red-500 font-bold text-center">${data.message}</p>`);
        }
        setTimeout(closeModal, 3000);
    })
    .catch(error => {
        showModal('Error', '<p class="text-red-500 font-bold text-center">An error occurred. Please try again.</p>');
        console.error('Error submitting form:', error);
        setTimeout(closeModal, 3000);
    });
}