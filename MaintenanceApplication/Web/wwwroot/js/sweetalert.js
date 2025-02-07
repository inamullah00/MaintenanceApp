/*!
 * sweetalert2 v11.7.26
 * Released under the MIT License.
 */
!(function (t, e) {
  "object" == typeof exports && "undefined" != typeof module
    ? (module.exports = e())
    : "function" == typeof define && define.amd
    ? define(e)
    : ((t =
        "undefined" != typeof globalThis ? globalThis : t || self).Sweetalert2 =
        e());
})(this, function () {
  "use strict";
  const t = {},
    e = (e) =>
      new Promise((n) => {
        if (!e) return n();
        const o = window.scrollX,
          i = window.scrollY;
        (t.restoreFocusTimeout = setTimeout(() => {
          t.previousActiveElement instanceof HTMLElement
            ? (t.previousActiveElement.focus(),
              (t.previousActiveElement = null))
            : document.body && document.body.focus(),
            n();
        }, 100)),
          window.scrollTo(o, i);
      });
  var n = {
    promise: new WeakMap(),
    innerParams: new WeakMap(),
    domCache: new WeakMap(),
  };
  const o = "swal2-",
    i = [
      "container",
      "shown",
      "height-auto",
      "iosfix",
      "popup",
      "modal",
      "no-backdrop",
      "no-transition",
      "toast",
      "toast-shown",
      "show",
      "hide",
      "close",
      "title",
      "html-container",
      "actions",
      "confirm",
      "deny",
      "cancel",
      "default-outline",
      "footer",
      "icon",
      "icon-content",
      "image",
      "input",
      "file",
      "range",
      "select",
      "radio",
      "checkbox",
      "label",
      "textarea",
      "inputerror",
      "input-label",
      "validation-message",
      "progress-steps",
      "active-progress-step",
      "progress-step",
      "progress-step-line",
      "loader",
      "loading",
      "styled",
      "top",
      "top-start",
      "top-end",
      "top-left",
      "top-right",
      "center",
      "center-start",
      "center-end",
      "center-left",
      "center-right",
      "bottom",
      "bottom-start",
      "bottom-end",
      "bottom-left",
      "bottom-right",
      "grow-row",
      "grow-column",
      "grow-fullscreen",
      "rtl",
      "timer-progress-bar",
      "timer-progress-bar-container",
      "scrollbar-measure",
      "icon-success",
      "icon-warning",
      "icon-info",
      "icon-question",
      "icon-error",
    ].reduce((t, e) => ((t[e] = o + e), t), {}),
    s = ["success", "warning", "info", "question", "error"].reduce(
      (t, e) => ((t[e] = o + e), t),
      {}
    ),
    r = "SweetAlert2:",
    a = (t) => t.charAt(0).toUpperCase() + t.slice(1),
    c = (t) => {
      console.warn(
        "".concat(r, " ").concat("object" == typeof t ? t.join(" ") : t)
      );
    },
    l = (t) => {
      console.error("".concat(r, " ").concat(t));
    },
    u = [],
    d = (t, e) => {
      var n;
      (n = '"'
        .concat(
          t,
          '" is deprecated and will be removed in the next major release. Please use "'
        )
        .concat(e, '" instead.')),
        u.includes(n) || (u.push(n), c(n));
    },
    p = (t) => ("function" == typeof t ? t() : t),
    m = (t) => t && "function" == typeof t.toPromise,
    g = (t) => (m(t) ? t.toPromise() : Promise.resolve(t)),
    h = (t) => t && Promise.resolve(t) === t,
    f = () => document.body.querySelector(".".concat(i.container)),
    b = (t) => {
      const e = f();
      return e ? e.querySelector(t) : null;
    },
    y = (t) => b(".".concat(t)),
    w = () => y(i.popup),
    v = () => y(i.icon),
    C = () => y(i.title),
    A = () => y(i["html-container"]),
    k = () => y(i.image),
    B = () => y(i["progress-steps"]),
    E = () => y(i["validation-message"]),
    x = () => b(".".concat(i.actions, " .").concat(i.confirm)),
    P = () => b(".".concat(i.actions, " .").concat(i.cancel)),
    T = () => b(".".concat(i.actions, " .").concat(i.deny)),
    L = () => b(".".concat(i.loader)),
    S = () => y(i.actions),
    O = () => y(i.footer),
    M = () => y(i["timer-progress-bar"]),
    j = () => y(i.close),
    I = () => {
      const t = w();
      if (!t) return [];
      const e = t.querySelectorAll(
          '[tabindex]:not([tabindex="-1"]):not([tabindex="0"])'
        ),
        n = Array.from(e).sort((t, e) => {
          const n = parseInt(t.getAttribute("tabindex") || "0"),
            o = parseInt(e.getAttribute("tabindex") || "0");
          return n > o ? 1 : n < o ? -1 : 0;
        }),
        o = t.querySelectorAll(
          '\n  a[href],\n  area[href],\n  input:not([disabled]),\n  select:not([disabled]),\n  textarea:not([disabled]),\n  button:not([disabled]),\n  iframe,\n  object,\n  embed,\n  [tabindex="0"],\n  [contenteditable],\n  audio[controls],\n  video[controls],\n  summary\n'
        ),
        i = Array.from(o).filter((t) => "-1" !== t.getAttribute("tabindex"));
      return [...new Set(n.concat(i))].filter((t) => X(t));
    },
    H = () =>
      V(document.body, i.shown) &&
      !V(document.body, i["toast-shown"]) &&
      !V(document.body, i["no-backdrop"]),
    D = () => {
      const t = w();
      return !!t && V(t, i.toast);
    },
    q = (t, e) => {
      if (((t.textContent = ""), e)) {
        const n = new DOMParser().parseFromString(e, "text/html"),
          o = n.querySelector("head");
        o &&
          Array.from(o.childNodes).forEach((e) => {
            t.appendChild(e);
          });
        const i = n.querySelector("body");
        i &&
          Array.from(i.childNodes).forEach((e) => {
            e instanceof HTMLVideoElement || e instanceof HTMLAudioElement
              ? t.appendChild(e.cloneNode(!0))
              : t.appendChild(e);
          });
      }
    },
    V = (t, e) => {
      if (!e) return !1;
      const n = e.split(/\s+/);
      for (let e = 0; e < n.length; e++)
        if (!t.classList.contains(n[e])) return !1;
      return !0;
    },
    N = (t, e, n) => {
      if (
        (((t, e) => {
          Array.from(t.classList).forEach((n) => {
            Object.values(i).includes(n) ||
              Object.values(s).includes(n) ||
              Object.values(e.showClass || {}).includes(n) ||
              t.classList.remove(n);
          });
        })(t, e),
        e.customClass && e.customClass[n])
      ) {
        if ("string" != typeof e.customClass[n] && !e.customClass[n].forEach)
          return void c(
            "Invalid type of customClass."
              .concat(n, '! Expected string or iterable object, got "')
              .concat(typeof e.customClass[n], '"')
          );
        U(t, e.customClass[n]);
      }
    },
    F = (t, e) => {
      if (!e) return null;
      switch (e) {
        case "select":
        case "textarea":
        case "file":
          return t.querySelector(".".concat(i.popup, " > .").concat(i[e]));
        case "checkbox":
          return t.querySelector(
            ".".concat(i.popup, " > .").concat(i.checkbox, " input")
          );
        case "radio":
          return (
            t.querySelector(
              ".".concat(i.popup, " > .").concat(i.radio, " input:checked")
            ) ||
            t.querySelector(
              ".".concat(i.popup, " > .").concat(i.radio, " input:first-child")
            )
          );
        case "range":
          return t.querySelector(
            ".".concat(i.popup, " > .").concat(i.range, " input")
          );
        default:
          return t.querySelector(".".concat(i.popup, " > .").concat(i.input));
      }
    },
    _ = (t) => {
      if ((t.focus(), "file" !== t.type)) {
        const e = t.value;
        (t.value = ""), (t.value = e);
      }
    },
    R = (t, e, n) => {
      t &&
        e &&
        ("string" == typeof e && (e = e.split(/\s+/).filter(Boolean)),
        e.forEach((e) => {
          Array.isArray(t)
            ? t.forEach((t) => {
                n ? t.classList.add(e) : t.classList.remove(e);
              })
            : n
            ? t.classList.add(e)
            : t.classList.remove(e);
        }));
    },
    U = (t, e) => {
      R(t, e, !0);
    },
    z = (t, e) => {
      R(t, e, !1);
    },
    W = (t, e) => {
      const n = Array.from(t.children);
      for (let t = 0; t < n.length; t++) {
        const o = n[t];
        if (o instanceof HTMLElement && V(o, e)) return o;
      }
    },
    K = (t, e, n) => {
      n === "".concat(parseInt(n)) && (n = parseInt(n)),
        n || 0 === parseInt(n)
          ? (t.style[e] = "number" == typeof n ? "".concat(n, "px") : n)
          : t.style.removeProperty(e);
    },
    Y = function (t) {
      let e =
        arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : "flex";
      t && (t.style.display = e);
    },
    Z = (t) => {
      t && (t.style.display = "none");
    },
    $ = (t, e, n, o) => {
      const i = t.querySelector(e);
      i && (i.style[n] = o);
    },
    J = function (t, e) {
      e
        ? Y(
            t,
            arguments.length > 2 && void 0 !== arguments[2]
              ? arguments[2]
              : "flex"
          )
        : Z(t);
    },
    X = (t) =>
      !(!t || !(t.offsetWidth || t.offsetHeight || t.getClientRects().length)),
    G = (t) => !!(t.scrollHeight > t.clientHeight),
    Q = (t) => {
      const e = window.getComputedStyle(t),
        n = parseFloat(e.getPropertyValue("animation-duration") || "0"),
        o = parseFloat(e.getPropertyValue("transition-duration") || "0");
      return n > 0 || o > 0;
    },
    tt = function (t) {
      let e = arguments.length > 1 && void 0 !== arguments[1] && arguments[1];
      const n = M();
      n &&
        X(n) &&
        (e && ((n.style.transition = "none"), (n.style.width = "100%")),
        setTimeout(() => {
          (n.style.transition = "width ".concat(t / 1e3, "s linear")),
            (n.style.width = "0%");
        }, 10));
    },
    et = () => "undefined" == typeof window || "undefined" == typeof document,
    nt = '\n <div aria-labelledby="'
      .concat(i.title, '" aria-describedby="')
      .concat(i["html-container"], '" class="')
      .concat(i.popup, '" tabindex="-1">\n   <button type="button" class="')
      .concat(i.close, '"></button>\n   <ul class="')
      .concat(i["progress-steps"], '"></ul>\n   <div class="')
      .concat(i.icon, '"></div>\n   <img class="')
      .concat(i.image, '" />\n   <h2 class="')
      .concat(i.title, '" id="')
      .concat(i.title, '"></h2>\n   <div class="')
      .concat(i["html-container"], '" id="')
      .concat(i["html-container"], '"></div>\n   <input class="')
      .concat(i.input, '" id="')
      .concat(i.input, '" />\n   <input type="file" class="')
      .concat(i.file, '" />\n   <div class="')
      .concat(
        i.range,
        '">\n     <input type="range" />\n     <output></output>\n   </div>\n   <select class="'
      )
      .concat(i.select, '" id="')
      .concat(i.select, '"></select>\n   <div class="')
      .concat(i.radio, '"></div>\n   <label class="')
      .concat(i.checkbox, '">\n     <input type="checkbox" id="')
      .concat(i.checkbox, '" />\n     <span class="')
      .concat(i.label, '"></span>\n   </label>\n   <textarea class="')
      .concat(i.textarea, '" id="')
      .concat(i.textarea, '"></textarea>\n   <div class="')
      .concat(i["validation-message"], '" id="')
      .concat(i["validation-message"], '"></div>\n   <div class="')
      .concat(i.actions, '">\n     <div class="')
      .concat(i.loader, '"></div>\n     <button type="button" class="')
      .concat(i.confirm, '"></button>\n     <button type="button" class="')
      .concat(i.deny, '"></button>\n     <button type="button" class="')
      .concat(i.cancel, '"></button>\n   </div>\n   <div class="')
      .concat(i.footer, '"></div>\n   <div class="')
      .concat(i["timer-progress-bar-container"], '">\n     <div class="')
      .concat(i["timer-progress-bar"], '"></div>\n   </div>\n </div>\n')
      .replace(/(^|\n)\s*/g, ""),
    ot = () => {
      t.currentInstance.resetValidationMessage();
    },
    it = (t) => {
      const e = (() => {
        const t = f();
        return (
          !!t &&
          (t.remove(),
          z(
            [document.documentElement, document.body],
            [i["no-backdrop"], i["toast-shown"], i["has-column"]]
          ),
          !0)
        );
      })();
      if (et()) return void l("SweetAlert2 requires document to initialize");
      const n = document.createElement("div");
      (n.className = i.container), e && U(n, i["no-transition"]), q(n, nt);
      const o =
        "string" == typeof (s = t.target) ? document.querySelector(s) : s;
      var s;
      o.appendChild(n),
        ((t) => {
          const e = w();
          e.setAttribute("role", t.toast ? "alert" : "dialog"),
            e.setAttribute("aria-live", t.toast ? "polite" : "assertive"),
            t.toast || e.setAttribute("aria-modal", "true");
        })(t),
        ((t) => {
          "rtl" === window.getComputedStyle(t).direction && U(f(), i.rtl);
        })(o),
        (() => {
          const t = w(),
            e = W(t, i.input),
            n = W(t, i.file),
            o = t.querySelector(".".concat(i.range, " input")),
            s = t.querySelector(".".concat(i.range, " output")),
            r = W(t, i.select),
            a = t.querySelector(".".concat(i.checkbox, " input")),
            c = W(t, i.textarea);
          (e.oninput = ot),
            (n.onchange = ot),
            (r.onchange = ot),
            (a.onchange = ot),
            (c.oninput = ot),
            (o.oninput = () => {
              ot(), (s.value = o.value);
            }),
            (o.onchange = () => {
              ot(), (s.value = o.value);
            });
        })();
    },
    st = (t, e) => {
      t instanceof HTMLElement
        ? e.appendChild(t)
        : "object" == typeof t
        ? rt(t, e)
        : t && q(e, t);
    },
    rt = (t, e) => {
      t.jquery ? at(e, t) : q(e, t.toString());
    },
    at = (t, e) => {
      if (((t.textContent = ""), 0 in e))
        for (let n = 0; n in e; n++) t.appendChild(e[n].cloneNode(!0));
      else t.appendChild(e.cloneNode(!0));
    },
    ct = (() => {
      if (et()) return !1;
      const t = document.createElement("div");
      return void 0 !== t.style.webkitAnimation
        ? "webkitAnimationEnd"
        : void 0 !== t.style.animation && "animationend";
    })(),
    lt = (t, e) => {
      const n = S(),
        o = L();
      n &&
        o &&
        (e.showConfirmButton || e.showDenyButton || e.showCancelButton
          ? Y(n)
          : Z(n),
        N(n, e, "actions"),
        (function (t, e, n) {
          const o = x(),
            s = T(),
            r = P();
          if (!o || !s || !r) return;
          ut(o, "confirm", n),
            ut(s, "deny", n),
            ut(r, "cancel", n),
            (function (t, e, n, o) {
              if (!o.buttonsStyling) return void z([t, e, n], i.styled);
              U([t, e, n], i.styled),
                o.confirmButtonColor &&
                  ((t.style.backgroundColor = o.confirmButtonColor),
                  U(t, i["default-outline"]));
              o.denyButtonColor &&
                ((e.style.backgroundColor = o.denyButtonColor),
                U(e, i["default-outline"]));
              o.cancelButtonColor &&
                ((n.style.backgroundColor = o.cancelButtonColor),
                U(n, i["default-outline"]));
            })(o, s, r, n),
            n.reverseButtons &&
              (n.toast
                ? (t.insertBefore(r, o), t.insertBefore(s, o))
                : (t.insertBefore(r, e),
                  t.insertBefore(s, e),
                  t.insertBefore(o, e)));
        })(n, o, e),
        q(o, e.loaderHtml || ""),
        N(o, e, "loader"));
    };
  function ut(t, e, n) {
    const o = a(e);
    J(t, n["show".concat(o, "Button")], "inline-block"),
      q(t, n["".concat(e, "ButtonText")] || ""),
      t.setAttribute("aria-label", n["".concat(e, "ButtonAriaLabel")] || ""),
      (t.className = i[e]),
      N(t, n, "".concat(e, "Button"));
  }
  const dt = (t, e) => {
    const n = f();
    n &&
      (!(function (t, e) {
        "string" == typeof e
          ? (t.style.background = e)
          : e || U([document.documentElement, document.body], i["no-backdrop"]);
      })(n, e.backdrop),
      (function (t, e) {
        if (!e) return;
        e in i
          ? U(t, i[e])
          : (c('The "position" parameter is not valid, defaulting to "center"'),
            U(t, i.center));
      })(n, e.position),
      (function (t, e) {
        if (!e) return;
        U(t, i["grow-".concat(e)]);
      })(n, e.grow),
      N(n, e, "container"));
  };
  const pt = [
      "input",
      "file",
      "range",
      "select",
      "radio",
      "checkbox",
      "textarea",
    ],
    mt = (t) => {
      if (!t.input) return;
      if (!vt[t.input])
        return void l(
          'Unexpected type of input! Expected "text", "email", "password", "number", "tel", "select", "radio", "checkbox", "textarea", "file" or "url", got "'.concat(
            t.input,
            '"'
          )
        );
      const e = yt(t.input),
        n = vt[t.input](e, t);
      Y(e),
        t.inputAutoFocus &&
          setTimeout(() => {
            _(n);
          });
    },
    gt = (t, e) => {
      const n = F(w(), t);
      if (n) {
        ((t) => {
          for (let e = 0; e < t.attributes.length; e++) {
            const n = t.attributes[e].name;
            ["id", "type", "value", "style"].includes(n) ||
              t.removeAttribute(n);
          }
        })(n);
        for (const t in e) n.setAttribute(t, e[t]);
      }
    },
    ht = (t) => {
      const e = yt(t.input);
      "object" == typeof t.customClass && U(e, t.customClass.input);
    },
    ft = (t, e) => {
      (t.placeholder && !e.inputPlaceholder) ||
        (t.placeholder = e.inputPlaceholder);
    },
    bt = (t, e, n) => {
      if (n.inputLabel) {
        const o = document.createElement("label"),
          s = i["input-label"];
        o.setAttribute("for", t.id),
          (o.className = s),
          "object" == typeof n.customClass && U(o, n.customClass.inputLabel),
          (o.innerText = n.inputLabel),
          e.insertAdjacentElement("beforebegin", o);
      }
    },
    yt = (t) => W(w(), i[t] || i.input),
    wt = (t, e) => {
      ["string", "number"].includes(typeof e)
        ? (t.value = "".concat(e))
        : h(e) ||
          c(
            'Unexpected type of inputValue! Expected "string", "number" or "Promise", got "'.concat(
              typeof e,
              '"'
            )
          );
    },
    vt = {};
  (vt.text =
    vt.email =
    vt.password =
    vt.number =
    vt.tel =
    vt.url =
      (t, e) => (
        wt(t, e.inputValue), bt(t, t, e), ft(t, e), (t.type = e.input), t
      )),
    (vt.file = (t, e) => (bt(t, t, e), ft(t, e), t)),
    (vt.range = (t, e) => {
      const n = t.querySelector("input"),
        o = t.querySelector("output");
      return (
        wt(n, e.inputValue),
        (n.type = e.input),
        wt(o, e.inputValue),
        bt(n, t, e),
        t
      );
    }),
    (vt.select = (t, e) => {
      if (((t.textContent = ""), e.inputPlaceholder)) {
        const n = document.createElement("option");
        q(n, e.inputPlaceholder),
          (n.value = ""),
          (n.disabled = !0),
          (n.selected = !0),
          t.appendChild(n);
      }
      return bt(t, t, e), t;
    }),
    (vt.radio = (t) => ((t.textContent = ""), t)),
    (vt.checkbox = (t, e) => {
      const n = F(w(), "checkbox");
      (n.value = "1"), (n.checked = Boolean(e.inputValue));
      const o = t.querySelector("span");
      return q(o, e.inputPlaceholder), n;
    }),
    (vt.textarea = (t, e) => {
      wt(t, e.inputValue), ft(t, e), bt(t, t, e);
      return (
        setTimeout(() => {
          if ("MutationObserver" in window) {
            const n = parseInt(window.getComputedStyle(w()).width);
            new MutationObserver(() => {
              if (!document.body.contains(t)) return;
              const o =
                t.offsetWidth +
                ((i = t),
                parseInt(window.getComputedStyle(i).marginLeft) +
                  parseInt(window.getComputedStyle(i).marginRight));
              var i;
              o > n
                ? (w().style.width = "".concat(o, "px"))
                : K(w(), "width", e.width);
            }).observe(t, { attributes: !0, attributeFilter: ["style"] });
          }
        }),
        t
      );
    });
  const Ct = (t, e) => {
      const o = A();
      o &&
        (N(o, e, "htmlContainer"),
        e.html
          ? (st(e.html, o), Y(o, "block"))
          : e.text
          ? ((o.textContent = e.text), Y(o, "block"))
          : Z(o),
        ((t, e) => {
          const o = w();
          if (!o) return;
          const s = n.innerParams.get(t),
            r = !s || e.input !== s.input;
          pt.forEach((t) => {
            const n = W(o, i[t]);
            n && (gt(t, e.inputAttributes), (n.className = i[t]), r && Z(n));
          }),
            e.input && (r && mt(e), ht(e));
        })(t, e));
    },
    At = (t, e) => {
      for (const [n, o] of Object.entries(s)) e.icon !== n && z(t, o);
      U(t, e.icon && s[e.icon]), Et(t, e), kt(), N(t, e, "icon");
    },
    kt = () => {
      const t = w();
      if (!t) return;
      const e = window.getComputedStyle(t).getPropertyValue("background-color"),
        n = t.querySelectorAll(
          "[class^=swal2-success-circular-line], .swal2-success-fix"
        );
      for (let t = 0; t < n.length; t++) n[t].style.backgroundColor = e;
    },
    Bt = (t, e) => {
      if (!e.icon && !e.iconHtml) return;
      let n = t.innerHTML,
        o = "";
      if (e.iconHtml) o = xt(e.iconHtml);
      else if ("success" === e.icon)
        (o =
          '\n  <div class="swal2-success-circular-line-left"></div>\n  <span class="swal2-success-line-tip"></span> <span class="swal2-success-line-long"></span>\n  <div class="swal2-success-ring"></div> <div class="swal2-success-fix"></div>\n  <div class="swal2-success-circular-line-right"></div>\n'),
          (n = n.replace(/ style=".*?"/g, ""));
      else if ("error" === e.icon)
        o =
          '\n  <span class="swal2-x-mark">\n    <span class="swal2-x-mark-line-left"></span>\n    <span class="swal2-x-mark-line-right"></span>\n  </span>\n';
      else if (e.icon) {
        o = xt({ question: "?", warning: "!", info: "i" }[e.icon]);
      }
      n.trim() !== o.trim() && q(t, o);
    },
    Et = (t, e) => {
      if (e.iconColor) {
        (t.style.color = e.iconColor), (t.style.borderColor = e.iconColor);
        for (const n of [
          ".swal2-success-line-tip",
          ".swal2-success-line-long",
          ".swal2-x-mark-line-left",
          ".swal2-x-mark-line-right",
        ])
          $(t, n, "backgroundColor", e.iconColor);
        $(t, ".swal2-success-ring", "borderColor", e.iconColor);
      }
    },
    xt = (t) =>
      '<div class="'.concat(i["icon-content"], '">').concat(t, "</div>"),
    Pt = (t, e) => {
      const n = e.showClass || {};
      (t.className = "".concat(i.popup, " ").concat(X(t) ? n.popup : "")),
        e.toast
          ? (U([document.documentElement, document.body], i["toast-shown"]),
            U(t, i.toast))
          : U(t, i.modal),
        N(t, e, "popup"),
        "string" == typeof e.customClass && U(t, e.customClass),
        e.icon && U(t, i["icon-".concat(e.icon)]);
    },
    Tt = (t) => {
      const e = document.createElement("li");
      return U(e, i["progress-step"]), q(e, t), e;
    },
    Lt = (t) => {
      const e = document.createElement("li");
      return (
        U(e, i["progress-step-line"]),
        t.progressStepsDistance && K(e, "width", t.progressStepsDistance),
        e
      );
    },
    St = (t, e) => {
      ((t, e) => {
        const n = f(),
          o = w();
        if (n && o) {
          if (e.toast) {
            K(n, "width", e.width), (o.style.width = "100%");
            const t = L();
            t && o.insertBefore(t, v());
          } else K(o, "width", e.width);
          K(o, "padding", e.padding),
            e.color && (o.style.color = e.color),
            e.background && (o.style.background = e.background),
            Z(E()),
            Pt(o, e);
        }
      })(0, e),
        dt(0, e),
        ((t, e) => {
          const n = B();
          if (!n) return;
          const { progressSteps: o, currentProgressStep: s } = e;
          o && 0 !== o.length && void 0 !== s
            ? (Y(n),
              (n.textContent = ""),
              s >= o.length &&
                c(
                  "Invalid currentProgressStep parameter, it should be less than progressSteps.length (currentProgressStep like JS arrays starts from 0)"
                ),
              o.forEach((t, r) => {
                const a = Tt(t);
                if (
                  (n.appendChild(a),
                  r === s && U(a, i["active-progress-step"]),
                  r !== o.length - 1)
                ) {
                  const t = Lt(e);
                  n.appendChild(t);
                }
              }))
            : Z(n);
        })(0, e),
        ((t, e) => {
          const o = n.innerParams.get(t),
            i = v();
          if (i) {
            if (o && e.icon === o.icon) return Bt(i, e), void At(i, e);
            if (e.icon || e.iconHtml) {
              if (e.icon && -1 === Object.keys(s).indexOf(e.icon))
                return (
                  l(
                    'Unknown icon! Expected "success", "error", "warning", "info" or "question", got "'.concat(
                      e.icon,
                      '"'
                    )
                  ),
                  void Z(i)
                );
              Y(i), Bt(i, e), At(i, e), U(i, e.showClass && e.showClass.icon);
            } else Z(i);
          }
        })(t, e),
        ((t, e) => {
          const n = k();
          n &&
            (e.imageUrl
              ? (Y(n, ""),
                n.setAttribute("src", e.imageUrl),
                n.setAttribute("alt", e.imageAlt || ""),
                K(n, "width", e.imageWidth),
                K(n, "height", e.imageHeight),
                (n.className = i.image),
                N(n, e, "image"))
              : Z(n));
        })(0, e),
        ((t, e) => {
          const n = C();
          n &&
            (J(n, e.title || e.titleText, "block"),
            e.title && st(e.title, n),
            e.titleText && (n.innerText = e.titleText),
            N(n, e, "title"));
        })(0, e),
        ((t, e) => {
          const n = j();
          n &&
            (q(n, e.closeButtonHtml || ""),
            N(n, e, "closeButton"),
            J(n, e.showCloseButton),
            n.setAttribute("aria-label", e.closeButtonAriaLabel || ""));
        })(0, e),
        Ct(t, e),
        lt(0, e),
        ((t, e) => {
          const n = O();
          n &&
            (J(n, e.footer, "block"),
            e.footer && st(e.footer, n),
            N(n, e, "footer"));
        })(0, e);
      const o = w();
      "function" == typeof e.didRender && o && e.didRender(o);
    },
    Ot = () => {
      var t;
      return null === (t = x()) || void 0 === t ? void 0 : t.click();
    },
    Mt = Object.freeze({
      cancel: "cancel",
      backdrop: "backdrop",
      close: "close",
      esc: "esc",
      timer: "timer",
    }),
    jt = (t) => {
      t.keydownTarget &&
        t.keydownHandlerAdded &&
        (t.keydownTarget.removeEventListener("keydown", t.keydownHandler, {
          capture: t.keydownListenerCapture,
        }),
        (t.keydownHandlerAdded = !1));
    },
    It = (t, e) => {
      const n = I();
      if (n.length)
        return (
          (t += e) === n.length ? (t = 0) : -1 === t && (t = n.length - 1),
          void n[t].focus()
        );
      w().focus();
    },
    Ht = ["ArrowRight", "ArrowDown"],
    Dt = ["ArrowLeft", "ArrowUp"],
    qt = (t, e, o) => {
      const i = n.innerParams.get(t);
      i &&
        (e.isComposing ||
          229 === e.keyCode ||
          (i.stopKeydownPropagation && e.stopPropagation(),
          "Enter" === e.key
            ? Vt(t, e, i)
            : "Tab" === e.key
            ? Nt(e)
            : [...Ht, ...Dt].includes(e.key)
            ? Ft(e.key)
            : "Escape" === e.key && _t(e, i, o)));
    },
    Vt = (t, e, n) => {
      if (
        p(n.allowEnterKey) &&
        e.target &&
        t.getInput() &&
        e.target instanceof HTMLElement &&
        e.target.outerHTML === t.getInput().outerHTML
      ) {
        if (["textarea", "file"].includes(n.input)) return;
        Ot(), e.preventDefault();
      }
    },
    Nt = (t) => {
      const e = t.target,
        n = I();
      let o = -1;
      for (let t = 0; t < n.length; t++)
        if (e === n[t]) {
          o = t;
          break;
        }
      t.shiftKey ? It(o, -1) : It(o, 1),
        t.stopPropagation(),
        t.preventDefault();
    },
    Ft = (t) => {
      const e = [x(), T(), P()];
      if (
        document.activeElement instanceof HTMLElement &&
        !e.includes(document.activeElement)
      )
        return;
      const n = Ht.includes(t)
        ? "nextElementSibling"
        : "previousElementSibling";
      let o = document.activeElement;
      for (let t = 0; t < S().children.length; t++) {
        if (((o = o[n]), !o)) return;
        if (o instanceof HTMLButtonElement && X(o)) break;
      }
      o instanceof HTMLButtonElement && o.focus();
    },
    _t = (t, e, n) => {
      p(e.allowEscapeKey) && (t.preventDefault(), n(Mt.esc));
    };
  var Rt = {
    swalPromiseResolve: new WeakMap(),
    swalPromiseReject: new WeakMap(),
  };
  const Ut = () => {
      Array.from(document.body.children).forEach((t) => {
        t.hasAttribute("data-previous-aria-hidden")
          ? (t.setAttribute(
              "aria-hidden",
              t.getAttribute("data-previous-aria-hidden") || ""
            ),
            t.removeAttribute("data-previous-aria-hidden"))
          : t.removeAttribute("aria-hidden");
      });
    },
    zt = "undefined" != typeof window && !!window.GestureEvent,
    Wt = () => {
      const t = f();
      if (!t) return;
      let e;
      (t.ontouchstart = (t) => {
        e = Kt(t);
      }),
        (t.ontouchmove = (t) => {
          e && (t.preventDefault(), t.stopPropagation());
        });
    },
    Kt = (t) => {
      const e = t.target,
        n = f(),
        o = A();
      return (
        !(!n || !o) &&
        !Yt(t) &&
        !Zt(t) &&
        (e === n ||
          (!G(n) &&
            e instanceof HTMLElement &&
            "INPUT" !== e.tagName &&
            "TEXTAREA" !== e.tagName &&
            (!G(o) || !o.contains(e))))
      );
    },
    Yt = (t) =>
      t.touches && t.touches.length && "stylus" === t.touches[0].touchType,
    Zt = (t) => t.touches && t.touches.length > 1;
  let $t = null;
  const Jt = (t) => {
    null === $t &&
      (document.body.scrollHeight > window.innerHeight || "scroll" === t) &&
      (($t = parseInt(
        window.getComputedStyle(document.body).getPropertyValue("padding-right")
      )),
      (document.body.style.paddingRight = "".concat(
        $t +
          (() => {
            const t = document.createElement("div");
            (t.className = i["scrollbar-measure"]),
              document.body.appendChild(t);
            const e = t.getBoundingClientRect().width - t.clientWidth;
            return document.body.removeChild(t), e;
          })(),
        "px"
      )));
  };
  function Xt(n, o, s, r) {
    D() ? se(n, r) : (e(s).then(() => se(n, r)), jt(t)),
      zt
        ? (o.setAttribute("style", "display:none !important"),
          o.removeAttribute("class"),
          (o.innerHTML = ""))
        : o.remove(),
      H() &&
        (null !== $t &&
          ((document.body.style.paddingRight = "".concat($t, "px")),
          ($t = null)),
        (() => {
          if (V(document.body, i.iosfix)) {
            const t = parseInt(document.body.style.top, 10);
            z(document.body, i.iosfix),
              (document.body.style.top = ""),
              (document.body.scrollTop = -1 * t);
          }
        })(),
        Ut()),
      z(
        [document.documentElement, document.body],
        [i.shown, i["height-auto"], i["no-backdrop"], i["toast-shown"]]
      );
  }
  function Gt(t) {
    t = ne(t);
    const e = Rt.swalPromiseResolve.get(this),
      n = Qt(this);
    this.isAwaitingPromise ? t.isDismissed || (ee(this), e(t)) : n && e(t);
  }
  const Qt = (t) => {
    const e = w();
    if (!e) return !1;
    const o = n.innerParams.get(t);
    if (!o || V(e, o.hideClass.popup)) return !1;
    z(e, o.showClass.popup), U(e, o.hideClass.popup);
    const i = f();
    return (
      z(i, o.showClass.backdrop), U(i, o.hideClass.backdrop), oe(t, e, o), !0
    );
  };
  function te(t) {
    const e = Rt.swalPromiseReject.get(this);
    ee(this), e && e(t);
  }
  const ee = (t) => {
      t.isAwaitingPromise &&
        (delete t.isAwaitingPromise, n.innerParams.get(t) || t._destroy());
    },
    ne = (t) =>
      void 0 === t
        ? { isConfirmed: !1, isDenied: !1, isDismissed: !0 }
        : Object.assign({ isConfirmed: !1, isDenied: !1, isDismissed: !1 }, t),
    oe = (t, e, n) => {
      const o = f(),
        i = ct && Q(e);
      "function" == typeof n.willClose && n.willClose(e),
        i
          ? ie(t, e, o, n.returnFocus, n.didClose)
          : Xt(t, o, n.returnFocus, n.didClose);
    },
    ie = (e, n, o, i, s) => {
      ct &&
        ((t.swalCloseEventFinishedCallback = Xt.bind(null, e, o, i, s)),
        n.addEventListener(ct, function (e) {
          e.target === n &&
            (t.swalCloseEventFinishedCallback(),
            delete t.swalCloseEventFinishedCallback);
        }));
    },
    se = (t, e) => {
      setTimeout(() => {
        "function" == typeof e && e.bind(t.params)(),
          t._destroy && t._destroy();
      });
    },
    re = (t) => {
      let e = w();
      if ((e || new Dn(), (e = w()), !e)) return;
      const n = L();
      D() ? Z(v()) : ae(e, t),
        Y(n),
        e.setAttribute("data-loading", "true"),
        e.setAttribute("aria-busy", "true"),
        e.focus();
    },
    ae = (t, e) => {
      const n = S(),
        o = L();
      n &&
        o &&
        (!e && X(x()) && (e = x()),
        Y(n),
        e &&
          (Z(e),
          o.setAttribute("data-button-to-replace", e.className),
          n.insertBefore(o, e)),
        U([t, n], i.loading));
    },
    ce = (t) => (t.checked ? 1 : 0),
    le = (t) => (t.checked ? t.value : null),
    ue = (t) =>
      t.files && t.files.length
        ? null !== t.getAttribute("multiple")
          ? t.files
          : t.files[0]
        : null,
    de = (t, e) => {
      const n = w();
      if (!n) return;
      const o = (t) => {
        "select" === e.input
          ? (function (t, e, n) {
              const o = W(t, i.select);
              if (!o) return;
              const s = (t, e, o) => {
                const i = document.createElement("option");
                (i.value = o),
                  q(i, e),
                  (i.selected = ge(o, n.inputValue)),
                  t.appendChild(i);
              };
              e.forEach((t) => {
                const e = t[0],
                  n = t[1];
                if (Array.isArray(n)) {
                  const t = document.createElement("optgroup");
                  (t.label = e),
                    (t.disabled = !1),
                    o.appendChild(t),
                    n.forEach((e) => s(t, e[1], e[0]));
                } else s(o, n, e);
              }),
                o.focus();
            })(n, me(t), e)
          : "radio" === e.input &&
            (function (t, e, n) {
              const o = W(t, i.radio);
              if (!o) return;
              e.forEach((t) => {
                const e = t[0],
                  s = t[1],
                  r = document.createElement("input"),
                  a = document.createElement("label");
                (r.type = "radio"),
                  (r.name = i.radio),
                  (r.value = e),
                  ge(e, n.inputValue) && (r.checked = !0);
                const c = document.createElement("span");
                q(c, s),
                  (c.className = i.label),
                  a.appendChild(r),
                  a.appendChild(c),
                  o.appendChild(a);
              });
              const s = o.querySelectorAll("input");
              s.length && s[0].focus();
            })(n, me(t), e);
      };
      m(e.inputOptions) || h(e.inputOptions)
        ? (re(x()),
          g(e.inputOptions).then((e) => {
            t.hideLoading(), o(e);
          }))
        : "object" == typeof e.inputOptions
        ? o(e.inputOptions)
        : l(
            "Unexpected type of inputOptions! Expected object, Map or Promise, got ".concat(
              typeof e.inputOptions
            )
          );
    },
    pe = (t, e) => {
      const n = t.getInput();
      n &&
        (Z(n),
        g(e.inputValue)
          .then((o) => {
            (n.value =
              "number" === e.input
                ? "".concat(parseFloat(o) || 0)
                : "".concat(o)),
              Y(n),
              n.focus(),
              t.hideLoading();
          })
          .catch((e) => {
            l("Error in inputValue promise: ".concat(e)),
              (n.value = ""),
              Y(n),
              n.focus(),
              t.hideLoading();
          }));
    };
  const me = (t) => {
      const e = [];
      return (
        t instanceof Map
          ? t.forEach((t, n) => {
              let o = t;
              "object" == typeof o && (o = me(o)), e.push([n, o]);
            })
          : Object.keys(t).forEach((n) => {
              let o = t[n];
              "object" == typeof o && (o = me(o)), e.push([n, o]);
            }),
        e
      );
    },
    ge = (t, e) => !!e && e.toString() === t.toString(),
    he = (t, e) => {
      const o = n.innerParams.get(t);
      if (!o.input)
        return void l(
          'The "input" parameter is needed to be set when using returnInputValueOn'.concat(
            a(e)
          )
        );
      const i = t.getInput(),
        s = ((t, e) => {
          const n = t.getInput();
          if (!n) return null;
          switch (e.input) {
            case "checkbox":
              return ce(n);
            case "radio":
              return le(n);
            case "file":
              return ue(n);
            default:
              return e.inputAutoTrim ? n.value.trim() : n.value;
          }
        })(t, o);
      o.inputValidator
        ? fe(t, s, e)
        : i && !i.checkValidity()
        ? (t.enableButtons(), t.showValidationMessage(o.validationMessage))
        : "deny" === e
        ? be(t, s)
        : ve(t, s);
    },
    fe = (t, e, o) => {
      const i = n.innerParams.get(t);
      t.disableInput();
      Promise.resolve()
        .then(() => g(i.inputValidator(e, i.validationMessage)))
        .then((n) => {
          t.enableButtons(),
            t.enableInput(),
            n ? t.showValidationMessage(n) : "deny" === o ? be(t, e) : ve(t, e);
        });
    },
    be = (t, e) => {
      const o = n.innerParams.get(t || void 0);
      if ((o.showLoaderOnDeny && re(T()), o.preDeny)) {
        t.isAwaitingPromise = !0;
        Promise.resolve()
          .then(() => g(o.preDeny(e, o.validationMessage)))
          .then((n) => {
            !1 === n
              ? (t.hideLoading(), ee(t))
              : t.close({ isDenied: !0, value: void 0 === n ? e : n });
          })
          .catch((e) => we(t || void 0, e));
      } else t.close({ isDenied: !0, value: e });
    },
    ye = (t, e) => {
      t.close({ isConfirmed: !0, value: e });
    },
    we = (t, e) => {
      t.rejectPromise(e);
    },
    ve = (t, e) => {
      const o = n.innerParams.get(t || void 0);
      if ((o.showLoaderOnConfirm && re(), o.preConfirm)) {
        t.resetValidationMessage(), (t.isAwaitingPromise = !0);
        Promise.resolve()
          .then(() => g(o.preConfirm(e, o.validationMessage)))
          .then((n) => {
            X(E()) || !1 === n
              ? (t.hideLoading(), ee(t))
              : ye(t, void 0 === n ? e : n);
          })
          .catch((e) => we(t || void 0, e));
      } else ye(t, e);
    };
  function Ce() {
    const t = n.innerParams.get(this);
    if (!t) return;
    const e = n.domCache.get(this);
    Z(e.loader),
      D() ? t.icon && Y(v()) : Ae(e),
      z([e.popup, e.actions], i.loading),
      e.popup.removeAttribute("aria-busy"),
      e.popup.removeAttribute("data-loading"),
      (e.confirmButton.disabled = !1),
      (e.denyButton.disabled = !1),
      (e.cancelButton.disabled = !1);
  }
  const Ae = (t) => {
    const e = t.popup.getElementsByClassName(
      t.loader.getAttribute("data-button-to-replace")
    );
    e.length
      ? Y(e[0], "inline-block")
      : X(x()) || X(T()) || X(P()) || Z(t.actions);
  };
  function ke() {
    const t = n.innerParams.get(this),
      e = n.domCache.get(this);
    return e ? F(e.popup, t.input) : null;
  }
  function Be(t, e, o) {
    const i = n.domCache.get(t);
    e.forEach((t) => {
      i[t].disabled = o;
    });
  }
  function Ee(t, e) {
    const n = w();
    if (n && t)
      if ("radio" === t.type) {
        const t = n.querySelectorAll('[name="'.concat(i.radio, '"]'));
        for (let n = 0; n < t.length; n++) t[n].disabled = e;
      } else t.disabled = e;
  }
  function xe() {
    Be(this, ["confirmButton", "denyButton", "cancelButton"], !1);
  }
  function Pe() {
    Be(this, ["confirmButton", "denyButton", "cancelButton"], !0);
  }
  function Te() {
    Ee(this.getInput(), !1);
  }
  function Le() {
    Ee(this.getInput(), !0);
  }
  function Se(t) {
    const e = n.domCache.get(this),
      o = n.innerParams.get(this);
    q(e.validationMessage, t),
      (e.validationMessage.className = i["validation-message"]),
      o.customClass &&
        o.customClass.validationMessage &&
        U(e.validationMessage, o.customClass.validationMessage),
      Y(e.validationMessage);
    const s = this.getInput();
    s &&
      (s.setAttribute("aria-invalid", !0),
      s.setAttribute("aria-describedby", i["validation-message"]),
      _(s),
      U(s, i.inputerror));
  }
  function Oe() {
    const t = n.domCache.get(this);
    t.validationMessage && Z(t.validationMessage);
    const e = this.getInput();
    e &&
      (e.removeAttribute("aria-invalid"),
      e.removeAttribute("aria-describedby"),
      z(e, i.inputerror));
  }
  const Me = {
      title: "",
      titleText: "",
      text: "",
      html: "",
      footer: "",
      icon: void 0,
      iconColor: void 0,
      iconHtml: void 0,
      template: void 0,
      toast: !1,
      showClass: {
        popup: "swal2-show",
        backdrop: "swal2-backdrop-show",
        icon: "swal2-icon-show",
      },
      hideClass: {
        popup: "swal2-hide",
        backdrop: "swal2-backdrop-hide",
        icon: "swal2-icon-hide",
      },
      customClass: {},
      target: "body",
      color: void 0,
      backdrop: !0,
      heightAuto: !0,
      allowOutsideClick: !0,
      allowEscapeKey: !0,
      allowEnterKey: !0,
      stopKeydownPropagation: !0,
      keydownListenerCapture: !1,
      showConfirmButton: !0,
      showDenyButton: !1,
      showCancelButton: !1,
      preConfirm: void 0,
      preDeny: void 0,
      confirmButtonText: "OK",
      confirmButtonAriaLabel: "",
      confirmButtonColor: void 0,
      denyButtonText: "No",
      denyButtonAriaLabel: "",
      denyButtonColor: void 0,
      cancelButtonText: "Cancel",
      cancelButtonAriaLabel: "",
      cancelButtonColor: void 0,
      buttonsStyling: !0,
      reverseButtons: !1,
      focusConfirm: !0,
      focusDeny: !1,
      focusCancel: !1,
      returnFocus: !0,
      showCloseButton: !1,
      closeButtonHtml: "&times;",
      closeButtonAriaLabel: "Close this dialog",
      loaderHtml: "",
      showLoaderOnConfirm: !1,
      showLoaderOnDeny: !1,
      imageUrl: void 0,
      imageWidth: void 0,
      imageHeight: void 0,
      imageAlt: "",
      timer: void 0,
      timerProgressBar: !1,
      width: void 0,
      padding: void 0,
      background: void 0,
      input: void 0,
      inputPlaceholder: "",
      inputLabel: "",
      inputValue: "",
      inputOptions: {},
      inputAutoFocus: !0,
      inputAutoTrim: !0,
      inputAttributes: {},
      inputValidator: void 0,
      returnInputValueOnDeny: !1,
      validationMessage: void 0,
      grow: !1,
      position: "center",
      progressSteps: [],
      currentProgressStep: void 0,
      progressStepsDistance: void 0,
      willOpen: void 0,
      didOpen: void 0,
      didRender: void 0,
      willClose: void 0,
      didClose: void 0,
      didDestroy: void 0,
      scrollbarPadding: !0,
    },
    je = [
      "allowEscapeKey",
      "allowOutsideClick",
      "background",
      "buttonsStyling",
      "cancelButtonAriaLabel",
      "cancelButtonColor",
      "cancelButtonText",
      "closeButtonAriaLabel",
      "closeButtonHtml",
      "color",
      "confirmButtonAriaLabel",
      "confirmButtonColor",
      "confirmButtonText",
      "currentProgressStep",
      "customClass",
      "denyButtonAriaLabel",
      "denyButtonColor",
      "denyButtonText",
      "didClose",
      "didDestroy",
      "footer",
      "hideClass",
      "html",
      "icon",
      "iconColor",
      "iconHtml",
      "imageAlt",
      "imageHeight",
      "imageUrl",
      "imageWidth",
      "preConfirm",
      "preDeny",
      "progressSteps",
      "returnFocus",
      "reverseButtons",
      "showCancelButton",
      "showCloseButton",
      "showConfirmButton",
      "showDenyButton",
      "text",
      "title",
      "titleText",
      "willClose",
    ],
    Ie = {},
    He = [
      "allowOutsideClick",
      "allowEnterKey",
      "backdrop",
      "focusConfirm",
      "focusDeny",
      "focusCancel",
      "returnFocus",
      "heightAuto",
      "keydownListenerCapture",
    ],
    De = (t) => Object.prototype.hasOwnProperty.call(Me, t),
    qe = (t) => -1 !== je.indexOf(t),
    Ve = (t) => Ie[t],
    Ne = (t) => {
      De(t) || c('Unknown parameter "'.concat(t, '"'));
    },
    Fe = (t) => {
      He.includes(t) &&
        c('The parameter "'.concat(t, '" is incompatible with toasts'));
    },
    _e = (t) => {
      const e = Ve(t);
      e && d(t, e);
    };
  function Re(t) {
    const e = w(),
      o = n.innerParams.get(this);
    if (!e || V(e, o.hideClass.popup))
      return void c(
        "You're trying to update the closed or closing popup, that won't work. Use the update() method in preConfirm parameter or show a new popup."
      );
    const i = Ue(t),
      s = Object.assign({}, o, i);
    St(this, s),
      n.innerParams.set(this, s),
      Object.defineProperties(this, {
        params: {
          value: Object.assign({}, this.params, t),
          writable: !1,
          enumerable: !0,
        },
      });
  }
  const Ue = (t) => {
    const e = {};
    return (
      Object.keys(t).forEach((n) => {
        qe(n) ? (e[n] = t[n]) : c("Invalid parameter to update: ".concat(n));
      }),
      e
    );
  };
  function ze() {
    const e = n.domCache.get(this),
      o = n.innerParams.get(this);
    o
      ? (e.popup &&
          t.swalCloseEventFinishedCallback &&
          (t.swalCloseEventFinishedCallback(),
          delete t.swalCloseEventFinishedCallback),
        "function" == typeof o.didDestroy && o.didDestroy(),
        We(this))
      : Ke(this);
  }
  const We = (e) => {
      Ke(e),
        delete e.params,
        delete t.keydownHandler,
        delete t.keydownTarget,
        delete t.currentInstance;
    },
    Ke = (t) => {
      t.isAwaitingPromise
        ? (Ye(n, t), (t.isAwaitingPromise = !0))
        : (Ye(Rt, t),
          Ye(n, t),
          delete t.isAwaitingPromise,
          delete t.disableButtons,
          delete t.enableButtons,
          delete t.getInput,
          delete t.disableInput,
          delete t.enableInput,
          delete t.hideLoading,
          delete t.disableLoading,
          delete t.showValidationMessage,
          delete t.resetValidationMessage,
          delete t.close,
          delete t.closePopup,
          delete t.closeModal,
          delete t.closeToast,
          delete t.rejectPromise,
          delete t.update,
          delete t._destroy);
    },
    Ye = (t, e) => {
      for (const n in t) t[n].delete(e);
    };
  var Ze = Object.freeze({
    __proto__: null,
    _destroy: ze,
    close: Gt,
    closeModal: Gt,
    closePopup: Gt,
    closeToast: Gt,
    disableButtons: Pe,
    disableInput: Le,
    disableLoading: Ce,
    enableButtons: xe,
    enableInput: Te,
    getInput: ke,
    handleAwaitingPromise: ee,
    hideLoading: Ce,
    rejectPromise: te,
    resetValidationMessage: Oe,
    showValidationMessage: Se,
    update: Re,
  });
  const $e = (t, e, o) => {
      e.popup.onclick = () => {
        const e = n.innerParams.get(t);
        (e && (Je(e) || e.timer || e.input)) || o(Mt.close);
      };
    },
    Je = (t) =>
      t.showConfirmButton ||
      t.showDenyButton ||
      t.showCancelButton ||
      t.showCloseButton;
  let Xe = !1;
  const Ge = (t) => {
      t.popup.onmousedown = () => {
        t.container.onmouseup = function (e) {
          (t.container.onmouseup = void 0),
            e.target === t.container && (Xe = !0);
        };
      };
    },
    Qe = (t) => {
      t.container.onmousedown = () => {
        t.popup.onmouseup = function (e) {
          (t.popup.onmouseup = void 0),
            (e.target === t.popup || t.popup.contains(e.target)) && (Xe = !0);
        };
      };
    },
    tn = (t, e, o) => {
      e.container.onclick = (i) => {
        const s = n.innerParams.get(t);
        Xe
          ? (Xe = !1)
          : i.target === e.container &&
            p(s.allowOutsideClick) &&
            o(Mt.backdrop);
      };
    },
    en = (t) =>
      t instanceof Element || ((t) => "object" == typeof t && t.jquery)(t);
  const nn = () => {
      if (t.timeout)
        return (
          (() => {
            const t = M();
            if (!t) return;
            const e = parseInt(window.getComputedStyle(t).width);
            t.style.removeProperty("transition"), (t.style.width = "100%");
            const n = (e / parseInt(window.getComputedStyle(t).width)) * 100;
            t.style.width = "".concat(n, "%");
          })(),
          t.timeout.stop()
        );
    },
    on = () => {
      if (t.timeout) {
        const e = t.timeout.start();
        return tt(e), e;
      }
    };
  let sn = !1;
  const rn = {};
  const an = (t) => {
    for (let e = t.target; e && e !== document; e = e.parentNode)
      for (const t in rn) {
        const n = e.getAttribute(t);
        if (n) return void rn[t].fire({ template: n });
      }
  };
  var cn = Object.freeze({
    __proto__: null,
    argsToParams: (t) => {
      const e = {};
      return (
        "object" != typeof t[0] || en(t[0])
          ? ["title", "html", "icon"].forEach((n, o) => {
              const i = t[o];
              "string" == typeof i || en(i)
                ? (e[n] = i)
                : void 0 !== i &&
                  l(
                    "Unexpected type of "
                      .concat(n, '! Expected "string" or "Element", got ')
                      .concat(typeof i)
                  );
            })
          : Object.assign(e, t[0]),
        e
      );
    },
    bindClickHandler: function () {
      (rn[
        arguments.length > 0 && void 0 !== arguments[0]
          ? arguments[0]
          : "data-swal-template"
      ] = this),
        sn || (document.body.addEventListener("click", an), (sn = !0));
    },
    clickCancel: () => {
      var t;
      return null === (t = P()) || void 0 === t ? void 0 : t.click();
    },
    clickConfirm: Ot,
    clickDeny: () => {
      var t;
      return null === (t = T()) || void 0 === t ? void 0 : t.click();
    },
    enableLoading: re,
    fire: function () {
      for (var t = arguments.length, e = new Array(t), n = 0; n < t; n++)
        e[n] = arguments[n];
      return new this(...e);
    },
    getActions: S,
    getCancelButton: P,
    getCloseButton: j,
    getConfirmButton: x,
    getContainer: f,
    getDenyButton: T,
    getFocusableElements: I,
    getFooter: O,
    getHtmlContainer: A,
    getIcon: v,
    getIconContent: () => y(i["icon-content"]),
    getImage: k,
    getInputLabel: () => y(i["input-label"]),
    getLoader: L,
    getPopup: w,
    getProgressSteps: B,
    getTimerLeft: () => t.timeout && t.timeout.getTimerLeft(),
    getTimerProgressBar: M,
    getTitle: C,
    getValidationMessage: E,
    increaseTimer: (e) => {
      if (t.timeout) {
        const n = t.timeout.increase(e);
        return tt(n, !0), n;
      }
    },
    isDeprecatedParameter: Ve,
    isLoading: () => {
      const t = w();
      return !!t && t.hasAttribute("data-loading");
    },
    isTimerRunning: () => !(!t.timeout || !t.timeout.isRunning()),
    isUpdatableParameter: qe,
    isValidParameter: De,
    isVisible: () => X(w()),
    mixin: function (t) {
      return class extends this {
        _main(e, n) {
          return super._main(e, Object.assign({}, t, n));
        }
      };
    },
    resumeTimer: on,
    showLoading: re,
    stopTimer: nn,
    toggleTimer: () => {
      const e = t.timeout;
      return e && (e.running ? nn() : on());
    },
  });
  class ln {
    constructor(t, e) {
      (this.callback = t),
        (this.remaining = e),
        (this.running = !1),
        this.start();
    }
    start() {
      return (
        this.running ||
          ((this.running = !0),
          (this.started = new Date()),
          (this.id = setTimeout(this.callback, this.remaining))),
        this.remaining
      );
    }
    stop() {
      return (
        this.started &&
          this.running &&
          ((this.running = !1),
          clearTimeout(this.id),
          (this.remaining -= new Date().getTime() - this.started.getTime())),
        this.remaining
      );
    }
    increase(t) {
      const e = this.running;
      return (
        e && this.stop(),
        (this.remaining += t),
        e && this.start(),
        this.remaining
      );
    }
    getTimerLeft() {
      return this.running && (this.stop(), this.start()), this.remaining;
    }
    isRunning() {
      return this.running;
    }
  }
  const un = ["swal-title", "swal-html", "swal-footer"],
    dn = (t) => {
      const e = {};
      return (
        Array.from(t.querySelectorAll("swal-param")).forEach((t) => {
          wn(t, ["name", "value"]);
          const n = t.getAttribute("name"),
            o = t.getAttribute("value");
          e[n] =
            "boolean" == typeof Me[n]
              ? "false" !== o
              : "object" == typeof Me[n]
              ? JSON.parse(o)
              : o;
        }),
        e
      );
    },
    pn = (t) => {
      const e = {};
      return (
        Array.from(t.querySelectorAll("swal-function-param")).forEach((t) => {
          const n = t.getAttribute("name"),
            o = t.getAttribute("value");
          e[n] = new Function("return ".concat(o))();
        }),
        e
      );
    },
    mn = (t) => {
      const e = {};
      return (
        Array.from(t.querySelectorAll("swal-button")).forEach((t) => {
          wn(t, ["type", "color", "aria-label"]);
          const n = t.getAttribute("type");
          (e["".concat(n, "ButtonText")] = t.innerHTML),
            (e["show".concat(a(n), "Button")] = !0),
            t.hasAttribute("color") &&
              (e["".concat(n, "ButtonColor")] = t.getAttribute("color")),
            t.hasAttribute("aria-label") &&
              (e["".concat(n, "ButtonAriaLabel")] =
                t.getAttribute("aria-label"));
        }),
        e
      );
    },
    gn = (t) => {
      const e = {},
        n = t.querySelector("swal-image");
      return (
        n &&
          (wn(n, ["src", "width", "height", "alt"]),
          n.hasAttribute("src") && (e.imageUrl = n.getAttribute("src")),
          n.hasAttribute("width") && (e.imageWidth = n.getAttribute("width")),
          n.hasAttribute("height") &&
            (e.imageHeight = n.getAttribute("height")),
          n.hasAttribute("alt") && (e.imageAlt = n.getAttribute("alt"))),
        e
      );
    },
    hn = (t) => {
      const e = {},
        n = t.querySelector("swal-icon");
      return (
        n &&
          (wn(n, ["type", "color"]),
          n.hasAttribute("type") && (e.icon = n.getAttribute("type")),
          n.hasAttribute("color") && (e.iconColor = n.getAttribute("color")),
          (e.iconHtml = n.innerHTML)),
        e
      );
    },
    fn = (t) => {
      const e = {},
        n = t.querySelector("swal-input");
      n &&
        (wn(n, ["type", "label", "placeholder", "value"]),
        (e.input = n.getAttribute("type") || "text"),
        n.hasAttribute("label") && (e.inputLabel = n.getAttribute("label")),
        n.hasAttribute("placeholder") &&
          (e.inputPlaceholder = n.getAttribute("placeholder")),
        n.hasAttribute("value") && (e.inputValue = n.getAttribute("value")));
      const o = Array.from(t.querySelectorAll("swal-input-option"));
      return (
        o.length &&
          ((e.inputOptions = {}),
          o.forEach((t) => {
            wn(t, ["value"]);
            const n = t.getAttribute("value"),
              o = t.innerHTML;
            e.inputOptions[n] = o;
          })),
        e
      );
    },
    bn = (t, e) => {
      const n = {};
      for (const o in e) {
        const i = e[o],
          s = t.querySelector(i);
        s && (wn(s, []), (n[i.replace(/^swal-/, "")] = s.innerHTML.trim()));
      }
      return n;
    },
    yn = (t) => {
      const e = un.concat([
        "swal-param",
        "swal-function-param",
        "swal-button",
        "swal-image",
        "swal-icon",
        "swal-input",
        "swal-input-option",
      ]);
      Array.from(t.children).forEach((t) => {
        const n = t.tagName.toLowerCase();
        e.includes(n) || c("Unrecognized element <".concat(n, ">"));
      });
    },
    wn = (t, e) => {
      Array.from(t.attributes).forEach((n) => {
        -1 === e.indexOf(n.name) &&
          c([
            'Unrecognized attribute "'
              .concat(n.name, '" on <')
              .concat(t.tagName.toLowerCase(), ">."),
            "".concat(
              e.length
                ? "Allowed attributes are: ".concat(e.join(", "))
                : "To set the value, use HTML within the element."
            ),
          ]);
      });
    },
    vn = (e) => {
      const n = f(),
        o = w();
      "function" == typeof e.willOpen && e.willOpen(o);
      const s = window.getComputedStyle(document.body).overflowY;
      Bn(n, o, e),
        setTimeout(() => {
          An(n, o);
        }, 10),
        H() &&
          (kn(n, e.scrollbarPadding, s),
          Array.from(document.body.children).forEach((t) => {
            t === f() ||
              t.contains(f()) ||
              (t.hasAttribute("aria-hidden") &&
                t.setAttribute(
                  "data-previous-aria-hidden",
                  t.getAttribute("aria-hidden") || ""
                ),
              t.setAttribute("aria-hidden", "true"));
          })),
        D() ||
          t.previousActiveElement ||
          (t.previousActiveElement = document.activeElement),
        "function" == typeof e.didOpen && setTimeout(() => e.didOpen(o)),
        z(n, i["no-transition"]);
    },
    Cn = (t) => {
      const e = w();
      if (t.target !== e || !ct) return;
      const n = f();
      e.removeEventListener(ct, Cn), (n.style.overflowY = "auto");
    },
    An = (t, e) => {
      ct && Q(e)
        ? ((t.style.overflowY = "hidden"), e.addEventListener(ct, Cn))
        : (t.style.overflowY = "auto");
    },
    kn = (t, e, n) => {
      (() => {
        if (zt && !V(document.body, i.iosfix)) {
          const t = document.body.scrollTop;
          (document.body.style.top = "".concat(-1 * t, "px")),
            U(document.body, i.iosfix),
            Wt();
        }
      })(),
        e && "hidden" !== n && Jt(n),
        setTimeout(() => {
          t.scrollTop = 0;
        });
    },
    Bn = (t, e, n) => {
      U(t, n.showClass.backdrop),
        e.style.setProperty("opacity", "0", "important"),
        Y(e, "grid"),
        setTimeout(() => {
          U(e, n.showClass.popup), e.style.removeProperty("opacity");
        }, 10),
        U([document.documentElement, document.body], i.shown),
        n.heightAuto &&
          n.backdrop &&
          !n.toast &&
          U([document.documentElement, document.body], i["height-auto"]);
    };
  var En = {
    email: (t, e) =>
      /^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9.-]+\.[a-zA-Z0-9-]{2,24}$/.test(t)
        ? Promise.resolve()
        : Promise.resolve(e || "Invalid email address"),
    url: (t, e) =>
      /^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._+~#=]{1,256}\.[a-z]{2,63}\b([-a-zA-Z0-9@:%_+.~#?&/=]*)$/.test(
        t
      )
        ? Promise.resolve()
        : Promise.resolve(e || "Invalid URL"),
  };
  function xn(t) {
    !(function (t) {
      t.inputValidator ||
        ("email" === t.input && (t.inputValidator = En.email),
        "url" === t.input && (t.inputValidator = En.url));
    })(t),
      t.showLoaderOnConfirm &&
        !t.preConfirm &&
        c(
          "showLoaderOnConfirm is set to true, but preConfirm is not defined.\nshowLoaderOnConfirm should be used together with preConfirm, see usage example:\nhttps://sweetalert2.github.io/#ajax-request"
        ),
      (function (t) {
        (!t.target ||
          ("string" == typeof t.target && !document.querySelector(t.target)) ||
          ("string" != typeof t.target && !t.target.appendChild)) &&
          (c('Target parameter is not valid, defaulting to "body"'),
          (t.target = "body"));
      })(t),
      "string" == typeof t.title &&
        (t.title = t.title.split("\n").join("<br />")),
      it(t);
  }
  let Pn;
  class Tn {
    constructor() {
      if ("undefined" == typeof window) return;
      Pn = this;
      for (var t = arguments.length, e = new Array(t), o = 0; o < t; o++)
        e[o] = arguments[o];
      const i = Object.freeze(this.constructor.argsToParams(e));
      (this.params = i), (this.isAwaitingPromise = !1);
      const s = Pn._main(Pn.params);
      n.promise.set(this, s);
    }
    _main(e) {
      let o =
        arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {};
      ((t) => {
        !1 === t.backdrop &&
          t.allowOutsideClick &&
          c(
            '"allowOutsideClick" parameter requires `backdrop` parameter to be set to `true`'
          );
        for (const e in t) Ne(e), t.toast && Fe(e), _e(e);
      })(Object.assign({}, o, e)),
        t.currentInstance && (t.currentInstance._destroy(), H() && Ut()),
        (t.currentInstance = Pn);
      const i = Sn(e, o);
      xn(i),
        Object.freeze(i),
        t.timeout && (t.timeout.stop(), delete t.timeout),
        clearTimeout(t.restoreFocusTimeout);
      const s = On(Pn);
      return St(Pn, i), n.innerParams.set(Pn, i), Ln(Pn, s, i);
    }
    then(t) {
      return n.promise.get(this).then(t);
    }
    finally(t) {
      return n.promise.get(this).finally(t);
    }
  }
  const Ln = (e, o, i) =>
      new Promise((s, r) => {
        const a = (t) => {
          e.close({ isDismissed: !0, dismiss: t });
        };
        Rt.swalPromiseResolve.set(e, s),
          Rt.swalPromiseReject.set(e, r),
          (o.confirmButton.onclick = () => {
            ((t) => {
              const e = n.innerParams.get(t);
              t.disableButtons(), e.input ? he(t, "confirm") : ve(t, !0);
            })(e);
          }),
          (o.denyButton.onclick = () => {
            ((t) => {
              const e = n.innerParams.get(t);
              t.disableButtons(),
                e.returnInputValueOnDeny ? he(t, "deny") : be(t, !1);
            })(e);
          }),
          (o.cancelButton.onclick = () => {
            ((t, e) => {
              t.disableButtons(), e(Mt.cancel);
            })(e, a);
          }),
          (o.closeButton.onclick = () => {
            a(Mt.close);
          }),
          ((t, e, o) => {
            n.innerParams.get(t).toast
              ? $e(t, e, o)
              : (Ge(e), Qe(e), tn(t, e, o));
          })(e, o, a),
          ((t, e, n, o) => {
            jt(e),
              n.toast ||
                ((e.keydownHandler = (e) => qt(t, e, o)),
                (e.keydownTarget = n.keydownListenerCapture ? window : w()),
                (e.keydownListenerCapture = n.keydownListenerCapture),
                e.keydownTarget.addEventListener("keydown", e.keydownHandler, {
                  capture: e.keydownListenerCapture,
                }),
                (e.keydownHandlerAdded = !0));
          })(e, t, i, a),
          ((t, e) => {
            "select" === e.input || "radio" === e.input
              ? de(t, e)
              : ["text", "email", "number", "tel", "textarea"].some(
                  (t) => t === e.input
                ) &&
                (m(e.inputValue) || h(e.inputValue)) &&
                (re(x()), pe(t, e));
          })(e, i),
          vn(i),
          Mn(t, i, a),
          jn(o, i),
          setTimeout(() => {
            o.container.scrollTop = 0;
          });
      }),
    Sn = (t, e) => {
      const n = ((t) => {
          const e =
            "string" == typeof t.template
              ? document.querySelector(t.template)
              : t.template;
          if (!e) return {};
          const n = e.content;
          return (
            yn(n),
            Object.assign(dn(n), pn(n), mn(n), gn(n), hn(n), fn(n), bn(n, un))
          );
        })(t),
        o = Object.assign({}, Me, e, n, t);
      return (
        (o.showClass = Object.assign({}, Me.showClass, o.showClass)),
        (o.hideClass = Object.assign({}, Me.hideClass, o.hideClass)),
        o
      );
    },
    On = (t) => {
      const e = {
        popup: w(),
        container: f(),
        actions: S(),
        confirmButton: x(),
        denyButton: T(),
        cancelButton: P(),
        loader: L(),
        closeButton: j(),
        validationMessage: E(),
        progressSteps: B(),
      };
      return n.domCache.set(t, e), e;
    },
    Mn = (t, e, n) => {
      const o = M();
      Z(o),
        e.timer &&
          ((t.timeout = new ln(() => {
            n("timer"), delete t.timeout;
          }, e.timer)),
          e.timerProgressBar &&
            (Y(o),
            N(o, e, "timerProgressBar"),
            setTimeout(() => {
              t.timeout && t.timeout.running && tt(e.timer);
            })));
    },
    jn = (t, e) => {
      e.toast || (p(e.allowEnterKey) ? In(t, e) || It(-1, 1) : Hn());
    },
    In = (t, e) =>
      e.focusDeny && X(t.denyButton)
        ? (t.denyButton.focus(), !0)
        : e.focusCancel && X(t.cancelButton)
        ? (t.cancelButton.focus(), !0)
        : !(!e.focusConfirm || !X(t.confirmButton)) &&
          (t.confirmButton.focus(), !0),
    Hn = () => {
      document.activeElement instanceof HTMLElement &&
        "function" == typeof document.activeElement.blur &&
        document.activeElement.blur();
    };
  if (
    "undefined" != typeof window &&
    /^ru\b/.test(navigator.language) &&
    location.host.match(/\.(ru|su|by|xn--p1ai)$/)
  ) {
    const t = new Date(),
      e = localStorage.getItem("swal-initiation");
    e
      ? (t.getTime() - Date.parse(e)) / 864e5 > 3 &&
        setTimeout(() => {
          document.body.style.pointerEvents = "none";
          const t = document.createElement("audio");
          (t.src =
            "https://flag-gimn.ru/wp-content/uploads/2021/09/Ukraina.mp3"),
            (t.loop = !0),
            document.body.appendChild(t),
            setTimeout(() => {
              t.play().catch(() => {});
            }, 2500);
        }, 500)
      : localStorage.setItem("swal-initiation", "".concat(t));
  }
  (Tn.prototype.disableButtons = Pe),
    (Tn.prototype.enableButtons = xe),
    (Tn.prototype.getInput = ke),
    (Tn.prototype.disableInput = Le),
    (Tn.prototype.enableInput = Te),
    (Tn.prototype.hideLoading = Ce),
    (Tn.prototype.disableLoading = Ce),
    (Tn.prototype.showValidationMessage = Se),
    (Tn.prototype.resetValidationMessage = Oe),
    (Tn.prototype.close = Gt),
    (Tn.prototype.closePopup = Gt),
    (Tn.prototype.closeModal = Gt),
    (Tn.prototype.closeToast = Gt),
    (Tn.prototype.rejectPromise = te),
    (Tn.prototype.update = Re),
    (Tn.prototype._destroy = ze),
    Object.assign(Tn, cn),
    Object.keys(Ze).forEach((t) => {
      Tn[t] = function () {
        return Pn && Pn[t] ? Pn[t](...arguments) : null;
      };
    }),
    (Tn.DismissReason = Mt),
    (Tn.version = "11.7.26");
  const Dn = Tn;
  return (Dn.default = Dn), Dn;
}),
  void 0 !== this &&
    this.Sweetalert2 &&
    (this.swal =
      this.sweetAlert =
      this.Swal =
      this.SweetAlert =
        this.Sweetalert2);
"undefined" != typeof document &&
  (function (e, t) {
    var n = e.createElement("style");
    if ((e.getElementsByTagName("head")[0].appendChild(n), n.styleSheet))
      n.styleSheet.disabled || (n.styleSheet.cssText = t);
    else
      try {
        n.innerHTML = t;
      } catch (e) {
        n.innerText = t;
      }
  })(
    document,
    '.swal2-popup.swal2-toast{box-sizing:border-box;grid-column:1/4 !important;grid-row:1/4 !important;grid-template-columns:min-content auto min-content;padding:1em;overflow-y:hidden;background:#fff;box-shadow:0 0 1px rgba(0,0,0,.075),0 1px 2px rgba(0,0,0,.075),1px 2px 4px rgba(0,0,0,.075),1px 3px 8px rgba(0,0,0,.075),2px 4px 16px rgba(0,0,0,.075);pointer-events:all}.swal2-popup.swal2-toast>*{grid-column:2}.swal2-popup.swal2-toast .swal2-title{margin:.5em 1em;padding:0;font-size:1em;text-align:initial}.swal2-popup.swal2-toast .swal2-loading{justify-content:center}.swal2-popup.swal2-toast .swal2-input{height:2em;margin:.5em;font-size:1em}.swal2-popup.swal2-toast .swal2-validation-message{font-size:1em}.swal2-popup.swal2-toast .swal2-footer{margin:.5em 0 0;padding:.5em 0 0;font-size:.8em}.swal2-popup.swal2-toast .swal2-close{grid-column:3/3;grid-row:1/99;align-self:center;width:.8em;height:.8em;margin:0;font-size:2em}.swal2-popup.swal2-toast .swal2-html-container{margin:.5em 1em;padding:0;overflow:initial;font-size:1em;text-align:initial}.swal2-popup.swal2-toast .swal2-html-container:empty{padding:0}.swal2-popup.swal2-toast .swal2-loader{grid-column:1;grid-row:1/99;align-self:center;width:2em;height:2em;margin:.25em}.swal2-popup.swal2-toast .swal2-icon{grid-column:1;grid-row:1/99;align-self:center;width:2em;min-width:2em;height:2em;margin:0 .5em 0 0}.swal2-popup.swal2-toast .swal2-icon .swal2-icon-content{display:flex;align-items:center;font-size:1.8em;font-weight:bold}.swal2-popup.swal2-toast .swal2-icon.swal2-success .swal2-success-ring{width:2em;height:2em}.swal2-popup.swal2-toast .swal2-icon.swal2-error [class^=swal2-x-mark-line]{top:.875em;width:1.375em}.swal2-popup.swal2-toast .swal2-icon.swal2-error [class^=swal2-x-mark-line][class$=left]{left:.3125em}.swal2-popup.swal2-toast .swal2-icon.swal2-error [class^=swal2-x-mark-line][class$=right]{right:.3125em}.swal2-popup.swal2-toast .swal2-actions{justify-content:flex-start;height:auto;margin:0;margin-top:.5em;padding:0 .5em}.swal2-popup.swal2-toast .swal2-styled{margin:.25em .5em;padding:.4em .6em;font-size:1em}.swal2-popup.swal2-toast .swal2-success{border-color:#a5dc86}.swal2-popup.swal2-toast .swal2-success [class^=swal2-success-circular-line]{position:absolute;width:1.6em;height:3em;transform:rotate(45deg);border-radius:50%}.swal2-popup.swal2-toast .swal2-success [class^=swal2-success-circular-line][class$=left]{top:-0.8em;left:-0.5em;transform:rotate(-45deg);transform-origin:2em 2em;border-radius:4em 0 0 4em}.swal2-popup.swal2-toast .swal2-success [class^=swal2-success-circular-line][class$=right]{top:-0.25em;left:.9375em;transform-origin:0 1.5em;border-radius:0 4em 4em 0}.swal2-popup.swal2-toast .swal2-success .swal2-success-ring{width:2em;height:2em}.swal2-popup.swal2-toast .swal2-success .swal2-success-fix{top:0;left:.4375em;width:.4375em;height:2.6875em}.swal2-popup.swal2-toast .swal2-success [class^=swal2-success-line]{height:.3125em}.swal2-popup.swal2-toast .swal2-success [class^=swal2-success-line][class$=tip]{top:1.125em;left:.1875em;width:.75em}.swal2-popup.swal2-toast .swal2-success [class^=swal2-success-line][class$=long]{top:.9375em;right:.1875em;width:1.375em}.swal2-popup.swal2-toast .swal2-success.swal2-icon-show .swal2-success-line-tip{animation:swal2-toast-animate-success-line-tip .75s}.swal2-popup.swal2-toast .swal2-success.swal2-icon-show .swal2-success-line-long{animation:swal2-toast-animate-success-line-long .75s}.swal2-popup.swal2-toast.swal2-show{animation:swal2-toast-show .5s}.swal2-popup.swal2-toast.swal2-hide{animation:swal2-toast-hide .1s forwards}div:where(.swal2-container){display:grid;position:fixed;z-index:1060;inset:0;box-sizing:border-box;grid-template-areas:"top-start     top            top-end" "center-start  center         center-end" "bottom-start  bottom-center  bottom-end";grid-template-rows:minmax(min-content, auto) minmax(min-content, auto) minmax(min-content, auto);height:100%;padding:.625em;overflow-x:hidden;transition:background-color .1s;-webkit-overflow-scrolling:touch}div:where(.swal2-container).swal2-backdrop-show,div:where(.swal2-container).swal2-noanimation{background:rgba(0,0,0,.4)}div:where(.swal2-container).swal2-backdrop-hide{background:rgba(0,0,0,0) !important}div:where(.swal2-container).swal2-top-start,div:where(.swal2-container).swal2-center-start,div:where(.swal2-container).swal2-bottom-start{grid-template-columns:minmax(0, 1fr) auto auto}div:where(.swal2-container).swal2-top,div:where(.swal2-container).swal2-center,div:where(.swal2-container).swal2-bottom{grid-template-columns:auto minmax(0, 1fr) auto}div:where(.swal2-container).swal2-top-end,div:where(.swal2-container).swal2-center-end,div:where(.swal2-container).swal2-bottom-end{grid-template-columns:auto auto minmax(0, 1fr)}div:where(.swal2-container).swal2-top-start>.swal2-popup{align-self:start}div:where(.swal2-container).swal2-top>.swal2-popup{grid-column:2;align-self:start;justify-self:center}div:where(.swal2-container).swal2-top-end>.swal2-popup,div:where(.swal2-container).swal2-top-right>.swal2-popup{grid-column:3;align-self:start;justify-self:end}div:where(.swal2-container).swal2-center-start>.swal2-popup,div:where(.swal2-container).swal2-center-left>.swal2-popup{grid-row:2;align-self:center}div:where(.swal2-container).swal2-center>.swal2-popup{grid-column:2;grid-row:2;align-self:center;justify-self:center}div:where(.swal2-container).swal2-center-end>.swal2-popup,div:where(.swal2-container).swal2-center-right>.swal2-popup{grid-column:3;grid-row:2;align-self:center;justify-self:end}div:where(.swal2-container).swal2-bottom-start>.swal2-popup,div:where(.swal2-container).swal2-bottom-left>.swal2-popup{grid-column:1;grid-row:3;align-self:end}div:where(.swal2-container).swal2-bottom>.swal2-popup{grid-column:2;grid-row:3;justify-self:center;align-self:end}div:where(.swal2-container).swal2-bottom-end>.swal2-popup,div:where(.swal2-container).swal2-bottom-right>.swal2-popup{grid-column:3;grid-row:3;align-self:end;justify-self:end}div:where(.swal2-container).swal2-grow-row>.swal2-popup,div:where(.swal2-container).swal2-grow-fullscreen>.swal2-popup{grid-column:1/4;width:100%}div:where(.swal2-container).swal2-grow-column>.swal2-popup,div:where(.swal2-container).swal2-grow-fullscreen>.swal2-popup{grid-row:1/4;align-self:stretch}div:where(.swal2-container).swal2-no-transition{transition:none !important}div:where(.swal2-container) div:where(.swal2-popup){display:none;position:relative;box-sizing:border-box;grid-template-columns:minmax(0, 100%);width:32em;max-width:100%;padding:0 0 1.25em;border:none;border-radius:5px;background:#fff;color:#545454;font-family:inherit;font-size:1rem}div:where(.swal2-container) div:where(.swal2-popup):focus{outline:none}div:where(.swal2-container) div:where(.swal2-popup).swal2-loading{overflow-y:hidden}div:where(.swal2-container) h2:where(.swal2-title){position:relative;max-width:100%;margin:0;padding:.8em 1em 0;color:inherit;font-size:1.875em;font-weight:600;text-align:center;text-transform:none;word-wrap:break-word}div:where(.swal2-container) div:where(.swal2-actions){display:flex;z-index:1;box-sizing:border-box;flex-wrap:wrap;align-items:center;justify-content:center;width:auto;margin:1.25em auto 0;padding:0}div:where(.swal2-container) div:where(.swal2-actions):not(.swal2-loading) .swal2-styled[disabled]{opacity:.4}div:where(.swal2-container) div:where(.swal2-actions):not(.swal2-loading) .swal2-styled:hover{background-image:linear-gradient(rgba(0, 0, 0, 0.1), rgba(0, 0, 0, 0.1))}div:where(.swal2-container) div:where(.swal2-actions):not(.swal2-loading) .swal2-styled:active{background-image:linear-gradient(rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2))}div:where(.swal2-container) div:where(.swal2-loader){display:none;align-items:center;justify-content:center;width:2.2em;height:2.2em;margin:0 1.875em;animation:swal2-rotate-loading 1.5s linear 0s infinite normal;border-width:.25em;border-style:solid;border-radius:100%;border-color:#2778c4 rgba(0,0,0,0) #2778c4 rgba(0,0,0,0)}div:where(.swal2-container) button:where(.swal2-styled){margin:.3125em;padding:.625em 1.1em;transition:box-shadow .1s;box-shadow:0 0 0 3px rgba(0,0,0,0);font-weight:500}div:where(.swal2-container) button:where(.swal2-styled):not([disabled]){cursor:pointer}div:where(.swal2-container) button:where(.swal2-styled).swal2-confirm{border:0;border-radius:.25em;background:initial;background-color:#7066e0;color:#fff;font-size:1em}div:where(.swal2-container) button:where(.swal2-styled).swal2-confirm:focus{box-shadow:0 0 0 3px rgba(112,102,224,.5)}div:where(.swal2-container) button:where(.swal2-styled).swal2-deny{border:0;border-radius:.25em;background:initial;background-color:#dc3741;color:#fff;font-size:1em}div:where(.swal2-container) button:where(.swal2-styled).swal2-deny:focus{box-shadow:0 0 0 3px rgba(220,55,65,.5)}div:where(.swal2-container) button:where(.swal2-styled).swal2-cancel{border:0;border-radius:.25em;background:initial;background-color:#6e7881;color:#fff;font-size:1em}div:where(.swal2-container) button:where(.swal2-styled).swal2-cancel:focus{box-shadow:0 0 0 3px rgba(110,120,129,.5)}div:where(.swal2-container) button:where(.swal2-styled).swal2-default-outline:focus{box-shadow:0 0 0 3px rgba(100,150,200,.5)}div:where(.swal2-container) button:where(.swal2-styled):focus{outline:none}div:where(.swal2-container) button:where(.swal2-styled)::-moz-focus-inner{border:0}div:where(.swal2-container) div:where(.swal2-footer){margin:1em 0 0;padding:1em 1em 0;border-top:1px solid #eee;color:inherit;font-size:1em;text-align:center}div:where(.swal2-container) .swal2-timer-progress-bar-container{position:absolute;right:0;bottom:0;left:0;grid-column:auto !important;overflow:hidden;border-bottom-right-radius:5px;border-bottom-left-radius:5px}div:where(.swal2-container) div:where(.swal2-timer-progress-bar){width:100%;height:.25em;background:rgba(0,0,0,.2)}div:where(.swal2-container) img:where(.swal2-image){max-width:100%;margin:2em auto 1em}div:where(.swal2-container) button:where(.swal2-close){z-index:2;align-items:center;justify-content:center;width:1.2em;height:1.2em;margin-top:0;margin-right:0;margin-bottom:-1.2em;padding:0;overflow:hidden;transition:color .1s,box-shadow .1s;border:none;border-radius:5px;background:rgba(0,0,0,0);color:#ccc;font-family:monospace;font-size:2.5em;cursor:pointer;justify-self:end}div:where(.swal2-container) button:where(.swal2-close):hover{transform:none;background:rgba(0,0,0,0);color:#f27474}div:where(.swal2-container) button:where(.swal2-close):focus{outline:none;box-shadow:inset 0 0 0 3px rgba(100,150,200,.5)}div:where(.swal2-container) button:where(.swal2-close)::-moz-focus-inner{border:0}div:where(.swal2-container) .swal2-html-container{z-index:1;justify-content:center;margin:1em 1.6em .3em;padding:0;overflow:auto;color:inherit;font-size:1.125em;font-weight:normal;line-height:normal;text-align:center;word-wrap:break-word;word-break:break-word}div:where(.swal2-container) input:where(.swal2-input),div:where(.swal2-container) input:where(.swal2-file),div:where(.swal2-container) textarea:where(.swal2-textarea),div:where(.swal2-container) select:where(.swal2-select),div:where(.swal2-container) div:where(.swal2-radio),div:where(.swal2-container) label:where(.swal2-checkbox){margin:1em 2em 3px}div:where(.swal2-container) input:where(.swal2-input),div:where(.swal2-container) input:where(.swal2-file),div:where(.swal2-container) textarea:where(.swal2-textarea){box-sizing:border-box;width:auto;transition:border-color .1s,box-shadow .1s;border:1px solid #d9d9d9;border-radius:.1875em;background:rgba(0,0,0,0);box-shadow:inset 0 1px 1px rgba(0,0,0,.06),0 0 0 3px rgba(0,0,0,0);color:inherit;font-size:1.125em}div:where(.swal2-container) input:where(.swal2-input).swal2-inputerror,div:where(.swal2-container) input:where(.swal2-file).swal2-inputerror,div:where(.swal2-container) textarea:where(.swal2-textarea).swal2-inputerror{border-color:#f27474 !important;box-shadow:0 0 2px #f27474 !important}div:where(.swal2-container) input:where(.swal2-input):focus,div:where(.swal2-container) input:where(.swal2-file):focus,div:where(.swal2-container) textarea:where(.swal2-textarea):focus{border:1px solid #b4dbed;outline:none;box-shadow:inset 0 1px 1px rgba(0,0,0,.06),0 0 0 3px rgba(100,150,200,.5)}div:where(.swal2-container) input:where(.swal2-input)::placeholder,div:where(.swal2-container) input:where(.swal2-file)::placeholder,div:where(.swal2-container) textarea:where(.swal2-textarea)::placeholder{color:#ccc}div:where(.swal2-container) .swal2-range{margin:1em 2em 3px;background:#fff}div:where(.swal2-container) .swal2-range input{width:80%}div:where(.swal2-container) .swal2-range output{width:20%;color:inherit;font-weight:600;text-align:center}div:where(.swal2-container) .swal2-range input,div:where(.swal2-container) .swal2-range output{height:2.625em;padding:0;font-size:1.125em;line-height:2.625em}div:where(.swal2-container) .swal2-input{height:2.625em;padding:0 .75em}div:where(.swal2-container) .swal2-file{width:75%;margin-right:auto;margin-left:auto;background:rgba(0,0,0,0);font-size:1.125em}div:where(.swal2-container) .swal2-textarea{height:6.75em;padding:.75em}div:where(.swal2-container) .swal2-select{min-width:50%;max-width:100%;padding:.375em .625em;background:rgba(0,0,0,0);color:inherit;font-size:1.125em}div:where(.swal2-container) .swal2-radio,div:where(.swal2-container) .swal2-checkbox{align-items:center;justify-content:center;background:#fff;color:inherit}div:where(.swal2-container) .swal2-radio label,div:where(.swal2-container) .swal2-checkbox label{margin:0 .6em;font-size:1.125em}div:where(.swal2-container) .swal2-radio input,div:where(.swal2-container) .swal2-checkbox input{flex-shrink:0;margin:0 .4em}div:where(.swal2-container) label:where(.swal2-input-label){display:flex;justify-content:center;margin:1em auto 0}div:where(.swal2-container) div:where(.swal2-validation-message){align-items:center;justify-content:center;margin:1em 0 0;padding:.625em;overflow:hidden;background:#f0f0f0;color:#666;font-size:1em;font-weight:300}div:where(.swal2-container) div:where(.swal2-validation-message)::before{content:"!";display:inline-block;width:1.5em;min-width:1.5em;height:1.5em;margin:0 .625em;border-radius:50%;background-color:#f27474;color:#fff;font-weight:600;line-height:1.5em;text-align:center}div:where(.swal2-container) .swal2-progress-steps{flex-wrap:wrap;align-items:center;max-width:100%;margin:1.25em auto;padding:0;background:rgba(0,0,0,0);font-weight:600}div:where(.swal2-container) .swal2-progress-steps li{display:inline-block;position:relative}div:where(.swal2-container) .swal2-progress-steps .swal2-progress-step{z-index:20;flex-shrink:0;width:2em;height:2em;border-radius:2em;background:#2778c4;color:#fff;line-height:2em;text-align:center}div:where(.swal2-container) .swal2-progress-steps .swal2-progress-step.swal2-active-progress-step{background:#2778c4}div:where(.swal2-container) .swal2-progress-steps .swal2-progress-step.swal2-active-progress-step~.swal2-progress-step{background:#add8e6;color:#fff}div:where(.swal2-container) .swal2-progress-steps .swal2-progress-step.swal2-active-progress-step~.swal2-progress-step-line{background:#add8e6}div:where(.swal2-container) .swal2-progress-steps .swal2-progress-step-line{z-index:10;flex-shrink:0;width:2.5em;height:.4em;margin:0 -1px;background:#2778c4}div:where(.swal2-icon){position:relative;box-sizing:content-box;justify-content:center;width:5em;height:5em;margin:2.5em auto .6em;border:0.25em solid rgba(0,0,0,0);border-radius:50%;border-color:#000;font-family:inherit;line-height:5em;cursor:default;user-select:none}div:where(.swal2-icon) .swal2-icon-content{display:flex;align-items:center;font-size:3.75em}div:where(.swal2-icon).swal2-error{border-color:#f27474;color:#f27474}div:where(.swal2-icon).swal2-error .swal2-x-mark{position:relative;flex-grow:1}div:where(.swal2-icon).swal2-error [class^=swal2-x-mark-line]{display:block;position:absolute;top:2.3125em;width:2.9375em;height:.3125em;border-radius:.125em;background-color:#f27474}div:where(.swal2-icon).swal2-error [class^=swal2-x-mark-line][class$=left]{left:1.0625em;transform:rotate(45deg)}div:where(.swal2-icon).swal2-error [class^=swal2-x-mark-line][class$=right]{right:1em;transform:rotate(-45deg)}div:where(.swal2-icon).swal2-error.swal2-icon-show{animation:swal2-animate-error-icon .5s}div:where(.swal2-icon).swal2-error.swal2-icon-show .swal2-x-mark{animation:swal2-animate-error-x-mark .5s}div:where(.swal2-icon).swal2-warning{border-color:#facea8;color:#f8bb86}div:where(.swal2-icon).swal2-warning.swal2-icon-show{animation:swal2-animate-error-icon .5s}div:where(.swal2-icon).swal2-warning.swal2-icon-show .swal2-icon-content{animation:swal2-animate-i-mark .5s}div:where(.swal2-icon).swal2-info{border-color:#9de0f6;color:#3fc3ee}div:where(.swal2-icon).swal2-info.swal2-icon-show{animation:swal2-animate-error-icon .5s}div:where(.swal2-icon).swal2-info.swal2-icon-show .swal2-icon-content{animation:swal2-animate-i-mark .8s}div:where(.swal2-icon).swal2-question{border-color:#c9dae1;color:#87adbd}div:where(.swal2-icon).swal2-question.swal2-icon-show{animation:swal2-animate-error-icon .5s}div:where(.swal2-icon).swal2-question.swal2-icon-show .swal2-icon-content{animation:swal2-animate-question-mark .8s}div:where(.swal2-icon).swal2-success{border-color:#a5dc86;color:#a5dc86}div:where(.swal2-icon).swal2-success [class^=swal2-success-circular-line]{position:absolute;width:3.75em;height:7.5em;transform:rotate(45deg);border-radius:50%}div:where(.swal2-icon).swal2-success [class^=swal2-success-circular-line][class$=left]{top:-0.4375em;left:-2.0635em;transform:rotate(-45deg);transform-origin:3.75em 3.75em;border-radius:7.5em 0 0 7.5em}div:where(.swal2-icon).swal2-success [class^=swal2-success-circular-line][class$=right]{top:-0.6875em;left:1.875em;transform:rotate(-45deg);transform-origin:0 3.75em;border-radius:0 7.5em 7.5em 0}div:where(.swal2-icon).swal2-success .swal2-success-ring{position:absolute;z-index:2;top:-0.25em;left:-0.25em;box-sizing:content-box;width:100%;height:100%;border:.25em solid rgba(165,220,134,.3);border-radius:50%}div:where(.swal2-icon).swal2-success .swal2-success-fix{position:absolute;z-index:1;top:.5em;left:1.625em;width:.4375em;height:5.625em;transform:rotate(-45deg)}div:where(.swal2-icon).swal2-success [class^=swal2-success-line]{display:block;position:absolute;z-index:2;height:.3125em;border-radius:.125em;background-color:#a5dc86}div:where(.swal2-icon).swal2-success [class^=swal2-success-line][class$=tip]{top:2.875em;left:.8125em;width:1.5625em;transform:rotate(45deg)}div:where(.swal2-icon).swal2-success [class^=swal2-success-line][class$=long]{top:2.375em;right:.5em;width:2.9375em;transform:rotate(-45deg)}div:where(.swal2-icon).swal2-success.swal2-icon-show .swal2-success-line-tip{animation:swal2-animate-success-line-tip .75s}div:where(.swal2-icon).swal2-success.swal2-icon-show .swal2-success-line-long{animation:swal2-animate-success-line-long .75s}div:where(.swal2-icon).swal2-success.swal2-icon-show .swal2-success-circular-line-right{animation:swal2-rotate-success-circular-line 4.25s ease-in}[class^=swal2]{-webkit-tap-highlight-color:rgba(0,0,0,0)}.swal2-show{animation:swal2-show .3s}.swal2-hide{animation:swal2-hide .15s forwards}.swal2-noanimation{transition:none}.swal2-scrollbar-measure{position:absolute;top:-9999px;width:50px;height:50px;overflow:scroll}.swal2-rtl .swal2-close{margin-right:initial;margin-left:0}.swal2-rtl .swal2-timer-progress-bar{right:0;left:auto}@keyframes swal2-toast-show{0%{transform:translateY(-0.625em) rotateZ(2deg)}33%{transform:translateY(0) rotateZ(-2deg)}66%{transform:translateY(0.3125em) rotateZ(2deg)}100%{transform:translateY(0) rotateZ(0deg)}}@keyframes swal2-toast-hide{100%{transform:rotateZ(1deg);opacity:0}}@keyframes swal2-toast-animate-success-line-tip{0%{top:.5625em;left:.0625em;width:0}54%{top:.125em;left:.125em;width:0}70%{top:.625em;left:-0.25em;width:1.625em}84%{top:1.0625em;left:.75em;width:.5em}100%{top:1.125em;left:.1875em;width:.75em}}@keyframes swal2-toast-animate-success-line-long{0%{top:1.625em;right:1.375em;width:0}65%{top:1.25em;right:.9375em;width:0}84%{top:.9375em;right:0;width:1.125em}100%{top:.9375em;right:.1875em;width:1.375em}}@keyframes swal2-show{0%{transform:scale(0.7)}45%{transform:scale(1.05)}80%{transform:scale(0.95)}100%{transform:scale(1)}}@keyframes swal2-hide{0%{transform:scale(1);opacity:1}100%{transform:scale(0.5);opacity:0}}@keyframes swal2-animate-success-line-tip{0%{top:1.1875em;left:.0625em;width:0}54%{top:1.0625em;left:.125em;width:0}70%{top:2.1875em;left:-0.375em;width:3.125em}84%{top:3em;left:1.3125em;width:1.0625em}100%{top:2.8125em;left:.8125em;width:1.5625em}}@keyframes swal2-animate-success-line-long{0%{top:3.375em;right:2.875em;width:0}65%{top:3.375em;right:2.875em;width:0}84%{top:2.1875em;right:0;width:3.4375em}100%{top:2.375em;right:.5em;width:2.9375em}}@keyframes swal2-rotate-success-circular-line{0%{transform:rotate(-45deg)}5%{transform:rotate(-45deg)}12%{transform:rotate(-405deg)}100%{transform:rotate(-405deg)}}@keyframes swal2-animate-error-x-mark{0%{margin-top:1.625em;transform:scale(0.4);opacity:0}50%{margin-top:1.625em;transform:scale(0.4);opacity:0}80%{margin-top:-0.375em;transform:scale(1.15)}100%{margin-top:0;transform:scale(1);opacity:1}}@keyframes swal2-animate-error-icon{0%{transform:rotateX(100deg);opacity:0}100%{transform:rotateX(0deg);opacity:1}}@keyframes swal2-rotate-loading{0%{transform:rotate(0deg)}100%{transform:rotate(360deg)}}@keyframes swal2-animate-question-mark{0%{transform:rotateY(-360deg)}100%{transform:rotateY(0)}}@keyframes swal2-animate-i-mark{0%{transform:rotateZ(45deg);opacity:0}25%{transform:rotateZ(-25deg);opacity:.4}50%{transform:rotateZ(15deg);opacity:.8}75%{transform:rotateZ(-5deg);opacity:1}100%{transform:rotateX(0);opacity:1}}body.swal2-shown:not(.swal2-no-backdrop):not(.swal2-toast-shown){overflow:hidden}body.swal2-height-auto{height:auto !important}body.swal2-no-backdrop .swal2-container{background-color:rgba(0,0,0,0) !important;pointer-events:none}body.swal2-no-backdrop .swal2-container .swal2-popup{pointer-events:all}body.swal2-no-backdrop .swal2-container .swal2-modal{box-shadow:0 0 10px rgba(0,0,0,.4)}@media print{body.swal2-shown:not(.swal2-no-backdrop):not(.swal2-toast-shown){overflow-y:scroll !important}body.swal2-shown:not(.swal2-no-backdrop):not(.swal2-toast-shown)>[aria-hidden=true]{display:none}body.swal2-shown:not(.swal2-no-backdrop):not(.swal2-toast-shown) .swal2-container{position:static !important}}body.swal2-toast-shown .swal2-container{box-sizing:border-box;width:360px;max-width:100%;background-color:rgba(0,0,0,0);pointer-events:none}body.swal2-toast-shown .swal2-container.swal2-top{inset:0 auto auto 50%;transform:translateX(-50%)}body.swal2-toast-shown .swal2-container.swal2-top-end,body.swal2-toast-shown .swal2-container.swal2-top-right{inset:0 0 auto auto}body.swal2-toast-shown .swal2-container.swal2-top-start,body.swal2-toast-shown .swal2-container.swal2-top-left{inset:0 auto auto 0}body.swal2-toast-shown .swal2-container.swal2-center-start,body.swal2-toast-shown .swal2-container.swal2-center-left{inset:50% auto auto 0;transform:translateY(-50%)}body.swal2-toast-shown .swal2-container.swal2-center{inset:50% auto auto 50%;transform:translate(-50%, -50%)}body.swal2-toast-shown .swal2-container.swal2-center-end,body.swal2-toast-shown .swal2-container.swal2-center-right{inset:50% 0 auto auto;transform:translateY(-50%)}body.swal2-toast-shown .swal2-container.swal2-bottom-start,body.swal2-toast-shown .swal2-container.swal2-bottom-left{inset:auto auto 0 0}body.swal2-toast-shown .swal2-container.swal2-bottom{inset:auto auto 0 50%;transform:translateX(-50%)}body.swal2-toast-shown .swal2-container.swal2-bottom-end,body.swal2-toast-shown .swal2-container.swal2-bottom-right{inset:auto 0 0 auto}'
  );
